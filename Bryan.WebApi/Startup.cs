﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using BryanWu.Domain.Interface;
using BryanWu.Domain.Service;
using Bryan.WebApi.Common;
using Common.Autofac;
using Common.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Common.Log;
using log4net;
using Common;
using log4net.Config;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.SwaggerUI;
using Bryan.WebApi.Models.AppSettings;

namespace Bryan.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //AppSettings 参数配置
            var appSettingsSection = Configuration.GetSection("AppSettings");

            var jwtSettingsSection = Configuration.GetSection("JwtSettings");
            var appSettings = appSettingsSection.Get<AppSettings>();

            services.Configure<AppSettings>(appSettingsSection);
            services.Configure<JwtSettings>(jwtSettingsSection);
            services.Configure<UploadSettings>(Configuration.GetSection("Upload"));

            //注册数据库服务
            DBManager.ConnectionString = Configuration.GetConnectionString("mysql_hfmall");
            DBManager.isLocal = appSettings.IsLocal;
            //注册redis
            //RedisRepository._connectionString = Configuration.GetConnectionString("Redis_Hfmall"); //appSettings.Redis_Hfmall;
            //RedisRepository._databaseKey = Configuration.GetConnectionString("Redis_DatabaseKey"); //appSettings.Redis_DatabaseKey;
            var cacheStr = Configuration.GetConnectionString("Redis_Hfmall");
            var csredis = new CSRedis.CSRedisClient(cacheStr);
            RedisHelper.Initialization(csredis);

            //加载log4net日志配置文件
            LogHelper._repository = LogManager.CreateRepository(EnumLoggerReository.NETCoreRepository.ToString());
            XmlConfigurator.Configure(LogHelper._repository, new FileInfo("log4net.config"));

            //中突出显示的代码设置了 2.1 兼容性标志
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(opts => opts.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss");

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = " Bryan.WebApi",
                    Description = "by Bryan",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "BryanWu",
                        Email = string.Empty,
                        Url = "http://www.cnblogs.com/yilezhu/"
                    },
                    License = new License
                    {
                        Name = "许可证名字",
                        Url = "http://www.cnblogs.com/yilezhu/"
                    }
                });

                #region Token绑定到ConfigureServices
                //添加header验证信息
                //Json Token认证方式，此方式为全局添加
                var security = new Dictionary<string, IEnumerable<string>> { { "Bearer", new string[] { } }, };
                options.AddSecurityRequirement(security);
                //方案名称“Bryan.WebApi”可自定义，上下一致即可
                options.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入{token}\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = "header",//jwt默认存放Authorization信息的位置(请求头中)
                    Type = "apiKey"
                });
                #endregion

                // 为 Swagger JSON and UI设置xml文档注释路径
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
                var xmlPath = Path.Combine(basePath, "Bryan.WebApi.xml");
                options.IncludeXmlComments(xmlPath);

            });

            #region JWT认证  
            //TOMO 令牌过期后刷新，以及更改密码后令牌未过期的处理问题
            var jwtSettings = jwtSettingsSection.Get<JwtSettings>();
            services.AddAuthentication(opts =>
            {
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opts =>
            {
                opts.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = context =>
                    {
                        var auth = context.Request.Headers["Authorization"].FirstOrDefault();
                        if (!string.IsNullOrEmpty(auth))
                        {
                            var token = auth.Split(' ');
                            if (token.Length > 1)
                            {
                                var jwtToken = new JwtSecurityToken(token[1]);
                                var payload = jwtToken.Payload;
                                var nameName = payload.Where(p => p.Key == "name").Select(p => p.Value).FirstOrDefault();
                                var userid = payload.Where(p => p.Key == "userId").Select(p => p.Value).FirstOrDefault();
                                var exp = (long)payload.Where(p => p.Key == "exp").Select(p => p.Value).FirstOrDefault();
                                //TODO 时间戳转换为时间
                                if (DateTime.Now > DateTimeHelper.ConvertToCsharpTime(exp))
                                {
                                    Controllers.BaseController._userId = 0;
                                    Controllers.BaseController._userName = string.Empty;
                                    context.Fail("Unauthorized11");
                                }
                                else
                                {
                                    Controllers.BaseController._userId = ConvertHelper.Instance.ConvertToInt(userid);
                                    Controllers.BaseController._userName = nameName.ToString();
                                }
                            }
                        }
                        return Task.CompletedTask;
                    }
                };

                opts.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secretkey)),
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,
                };
            });

            #endregion

            //配置跨域处理
            services.AddCors(options =>
            {
                options.AddPolicy("any", builder =>
                {
                    builder.AllowAnyOrigin() //允许任何来源的主机访问
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();//指定处理cookie
                });
                //options.AddPolicy("any", builder =>
                //{
                //    builder.WithOrigins("https://localhost:5001")
                //    .AllowAnyMethod()
                //    .AllowAnyHeader()
                //    .AllowCredentials();
                //});
            });

            //autofac 注入
            return new AutofacServiceProvider(AutofacConfig.Init(services));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())//判断是否是开发环境
            {
                app.UseDeveloperExceptionPage();//当exception是调用异常处理页面
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(opts =>
            {
                opts.SwaggerEndpoint("/swagger/v1/swagger.json", "Bryan.Webapi V1");
                opts.RoutePrefix = string.Empty;//开启默认swagger/index.html路径
                //页面API文档格式 Full=全部展开， List=只展开列表, None=都不展开
                opts.DocExpansion(DocExpansion.None);
            });

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });



        }
    }
}
