using Bryan.Common.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bryan.Common.Extension
{
    /// <summary>
    /// Startup扩展类
    /// </summary>
    public static class StartupExtension
    {
        public static void AddService(this IServiceCollection services, SysConfig systemConfig)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
               .AddJsonOptions(opts => opts.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss");

            services.AddMvc(//opt => opt.UseCentralRoutePrefix(new RouteAttribute(systemConfig.Name))
            )
            //.AddWebApiConventions()
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            .AddJsonOptions(opt => opt.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss");

            SysConfig systemConfig2 = systemConfig;
            //services.AddMicroService(systemConfig2);
            services.AddCors(opt =>
            {
                opt.AddPolicy("AllowDomain", builder =>
                {
                    builder.WithOrigins(new string[] { "*" })
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                    .AllowCredentials();
                });
            });
        }
        
        public static IApplicationBuilder UseService(this IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime, SysConfig systemConfig)
        {
            //MicroServiceExtension.UseMicroService(app, env, lifetime, systemConfig);
            //if (string.IsNullOrEmpty(systemConfig.CacheConnectionString))
            //{
            //    throw new Exception("请指定Redis连接字符串");
            //}
            //RedisHelper.Initialization(new CSRedisClient(systemConfig.CacheConnectionString));
            app.UseCors("AllowDomain");
            //app.UseMiddleware(Array.Empty<object>());
            //app.UseMiddleware(Array.Empty<object>());
            //app.UseMiddleware(Array.Empty<object>());
            return app;
        }
    }
}
