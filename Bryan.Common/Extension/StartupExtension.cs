using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bryan.Common.Entity;
using Bryan.Common.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Bryan.Common.Extension
{
    /// <summary>
    /// Startup扩展类
    /// </summary>
    public static class StartupExtension
    {
        public static void AddService(this IServiceCollection services, SysConfig systemConfig)
        {
            WebApiCompatShimMvcBuilderExtensions.AddWebApiConventions(services.AddMvc(opt =>
            {
                opt.UseCentralRoutePrefix(new RouteAttribute(systemConfig.Name));
            }))
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddJsonOptions(x =>
            {
                //设置时间格式
                x.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                //设置转换属性
                //x.SerializerSettings.ContractResolver = new ContractResolverOverload();
            });

            #region JWT认证
            //JWT配置注入
            services.Configure<JwtSettings>(opt =>
            {
                opt.Audience = systemConfig.JwtSettings.Audience;
                opt.Expires = systemConfig.JwtSettings.Expires;
                opt.Issuer = systemConfig.JwtSettings.Issuer;
                opt.PrivateKey = systemConfig.JwtSettings.PrivateKey;
                opt.PublicKey = systemConfig.JwtSettings.PublicKey;
                opt.Secretkey = systemConfig.JwtSettings.Secretkey;
            });
            //TODO 令牌过期后刷新，以及更改密码后令牌未过期的处理问题
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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(systemConfig.JwtSettings.Secretkey)),
                    ValidateIssuer = true,
                    ValidIssuer = systemConfig.JwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = systemConfig.JwtSettings.Audience,
                };
            });
            #endregion
            SysConfig systemConfig2 = systemConfig;
            services.AddMicroService(systemConfig2);
            services.AddCors(opt =>
            {
                opt.AddPolicy("any", builder =>
                {
                    builder.AllowAnyOrigin() //允许任何来源的主机访问
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();//指定处理cookie
                });
                //opt.AddPolicy("AllowDomain", builder =>
                //{
                //    builder.WithOrigins(new string[] { "*" })
                //    .AllowAnyHeader()
                //    .AllowAnyMethod()
                //    .AllowAnyOrigin()
                //    .AllowCredentials();
                //});
            });
        }

        public static IApplicationBuilder UseService(this IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime, SysConfig systemConfig)
        {
            MicroServiceExtension.UseMicroService(app, env, lifetime, systemConfig);
            //app.UseCors("AllowDomain");
            app.UseExceptionMiddleware();
            return app;
        }
    }
}
