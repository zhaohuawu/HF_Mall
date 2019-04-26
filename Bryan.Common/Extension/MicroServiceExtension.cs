using Bryan.Common.Entity;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using System.Threading;

namespace Bryan.Common.Extension
{
    public static class MicroServiceExtension
    {
        public static void AddMicroService(this IServiceCollection services, SysConfig serviceConfig)
        {
            services.AddSwaggerGen(delegate (SwaggerGenOptions option)
            {
                option.SwaggerDoc(serviceConfig.Name.ToLower(), new Info
                {
                    Title = serviceConfig.DisplayName,
                    Version = serviceConfig.Version
                });
                string filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, serviceConfig.XmlName);
                option.IncludeXmlComments(filePath, false);
            });
            services.AddCors(delegate (CorsOptions opt)
            {
                opt.AddPolicy("AllowDomain", delegate (CorsPolicyBuilder builder)
                {
                    builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials();
                });
            });
        }
        
        //public static void UseMicroService(this IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime, SysConfig serviceInfo)
        //{
        //    new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        //    string serviceId = " " + serviceInfo.Name + "-" + serviceInfo.LocalAddress.Replace(':', '-');
        //    string address = serviceInfo.LocalAddress.Split(':', StringSplitOptions.None)[0];
        //    string s = serviceInfo.LocalAddress.Split(':', StringSplitOptions.None)[1];
        //    using (ConsulClient consulClient = new ConsulClient(delegate (ConsulClientConfiguration x)
        //    {
        //        x.Address = new Uri(serviceInfo.ServiceDiscoveryAddress);
        //    }))
        //    {
        //        AgentServiceRegistration agentServiceRegistration = new AgentServiceRegistration
        //        {
        //            Address = address,
        //            Port = int.Parse(s),
        //            ID = serviceId,
        //            Name = serviceInfo.Name,
        //            Check = new AgentServiceCheck
        //            {
        //                DeregisterCriticalServiceAfter = new TimeSpan?(TimeSpan.FromSeconds(5.0)),
        //                HTTP = "http://" + serviceInfo.LocalAddress + "/health",
        //                Interval = new TimeSpan?(TimeSpan.FromSeconds(2.0)),
        //                Timeout = new TimeSpan?(TimeSpan.FromSeconds(1.0))
        //            }
        //        };
        //        agentServiceRegistration.Tags = new string[]
        //        {
        //            serviceInfo.DisplayName
        //        };
        //        consulClient.Agent.ServiceRegister(agentServiceRegistration, default(CancellationToken)).Wait();
        //    }
        //    Action<ConsulClientConfiguration> <> 9__5;
        //    lifetime.ApplicationStopped.Register(delegate ()
        //    {
        //        Action<ConsulClientConfiguration> configOverride;
        //        if ((configOverride = <> 9__5) == null)
        //        {
        //            configOverride = (<> 9__5 = delegate (ConsulClientConfiguration x)
        //            {
        //                x.Address = new Uri(serviceInfo.ServiceDiscoveryAddress);
        //            });
        //        }
        //        using (ConsulClient consulClient2 = new ConsulClient(configOverride))
        //        {
        //            consulClient2.Agent.ServiceDeregister(serviceId, default(CancellationToken)).Wait();
        //        }
        //    });
        //    app.Map("/health", delegate (IApplicationBuilder ab)
        //    {
        //        ab.Run(async delegate (HttpContext context)
        //        {
        //            await context.Response.WriteAsync("ok", default(CancellationToken));
        //        });
        //    });
        //    if (!env.IsProduction())
        //    {
        //        app.UseSwagger(delegate (SwaggerOptions opt)
        //        {
        //            opt.RouteTemplate = "{documentName}-swagger.json";
        //        });
        //        app.UseSwaggerUI(delegate (SwaggerUIOptions opt)
        //        {
        //            opt.SwaggerEndpoint("/" + serviceInfo.Name.ToLower() + "-swagger.json", serviceInfo.DisplayName);
        //        });
        //    }
        //    app.UseAuthentication();
        //}

        // Token: 0x06000004 RID: 4 RVA: 0x00002334 File Offset: 0x00000534
        public static void UseMicroService(this IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime, Action<SysConfig> action)
        {
            SysConfig sysConfig = new SysConfig();
            action(sysConfig);
            //app.UseMicroService(env, lifetime, sysConfig);
        }
    }
}
