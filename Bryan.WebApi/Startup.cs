using System;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Bryan.Common.Autofac;
using Bryan.Common.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Bryan.Common;
using Bryan.WebApi.Models.AppSettings;
using Bryan.Common.Jwt;
using Microsoft.Extensions.Logging;
using Bryan.Common.Entity;
using Bryan.Common.Extension;

namespace Bryan.WebApi
{
    public class Startup
    {
        private SysConfig config;
        public Startup(IConfiguration configuration, IHostingEnvironment env
            , ILogger<Startup> logger)
        {
            Configuration = configuration;
            Env = env;
            _logger = logger;
            config = new SysConfig();
            config.Name = "base";
            config.DisplayName = "基础服务";
            config.Version = "1.0";
            config.XmlName = "Bryan.WebApi.xml"; //当前项目 ->属性->生成->输出->勾选XML文档文件,并将XML文件名赋值在这.  
            //宿主机物理网卡地址
            config.LocalAddress = HttpContextExtension.GetLocalIP() + ":" + Configuration.GetSection("ServiceAddress").Value.Split(':')[1];
            //服务发现地址
            config.ServiceDiscoveryAddress = Configuration.GetSection("DCAddress").Value;
            config.RedisConnectionString = Configuration.GetConnectionString("Redis_Hfmall");
            config.RedisDefaultKey = Configuration.GetConnectionString("RedisDafaultKey");
            config.JwtSettings = Configuration.GetSection("JwtSettings").Get<JwtSettings>();
        }

        public IConfiguration Configuration { get; }
        private IHostingEnvironment Env { get; }
        private ILogger _logger;

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //AppSettings 参数配置
            var appSettingsSection = Configuration.GetSection("AppSettings");
            var appSettings = appSettingsSection.Get<AppSettings>();
            services.Configure<AppSettings>(appSettingsSection);
            services.Configure<UploadSettings>(Configuration.GetSection("Upload"));

            //注册数据库服务
            DBManager.ConnectionString = Configuration.GetConnectionString("mysql_hfmall");
            DBManager.isLocal = appSettings.IsLocal;
            _logger.LogWarning($"连接字符串串:{DBManager.ConnectionString}");

            services.AddService(config);

            //autofac 注入
            return new AutofacServiceProvider(AutofacConfig.Init(services));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())//判断是否是开发环境
            {
                app.UseDeveloperExceptionPage();//当exception是调用异常处理页面
            }
            else
            {
                app.UseHsts();
            }
            app.UseService(env, lifetime, config);
            //app.UseDefaultFiles();
            //app.UseStaticFiles();
            //app.UseCookiePolicy();
            app.UseMvc();
        }

        private void JwtValidation(IServiceCollection services)
        {
            var jwtSettingsSection = Configuration.GetSection("JwtSettings");
            services.Configure<JwtSettings>(jwtSettingsSection);
            //TODO 令牌过期后刷新，以及更改密码后令牌未过期的处理问题
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
                        var header = context.Request.Headers["Authorization"].FirstOrDefault();
                        var jwtEntity = JwtEntity.GetJwtIEntity(header);
                        if (jwtEntity != null)
                        {
                            if (DateTime.Now > DateTimeExtension.ConvertToCsharpTime(jwtEntity.Exp))
                            {
                                context.Fail("token已过期");
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
        }

        private void RegisterRedis()
        {
            var cacheStr = Configuration.GetConnectionString("Redis_Hfmall");
            var csredis = new CSRedis.CSRedisClient(cacheStr);
            RedisHelper.Initialization(csredis);
        }
    }
}
