using System;
using Bryan.Common.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

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
            //services.AddMvc()
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddJsonOptions(x =>
            {
                //设置时间格式
                x.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                //设置转换属性
                x.SerializerSettings.ContractResolver = new ContractResolverOverload();
            });

            SysConfig systemConfig2 = systemConfig;
            services.AddMicroService(systemConfig2);
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
            MicroServiceExtension.UseMicroService(app, env, lifetime, systemConfig);
            app.UseCors("AllowDomain");
            app.UseExceptionMiddleware();
            //app.UseMiddleware(Array.Empty<object>());
            //app.UseMiddleware(Array.Empty<object>());
            //app.UseMiddleware(Array.Empty<object>());
            return app;
        }
    }
}
