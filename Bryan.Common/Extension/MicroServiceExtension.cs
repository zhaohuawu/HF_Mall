using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
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


namespace Bryan.Common.Extension
{
    public static class MicroServiceExtension
    {
        public static void AddMicroService(this IServiceCollection services, SysConfig serviceConfig)
        {
            //redis注册
            if (string.IsNullOrEmpty(serviceConfig.RedisConnectionString))
            {
                throw new Exception("Redis链接字符串不能为空");
            }
            var csredis = new CSRedis.CSRedisClient(serviceConfig.RedisConnectionString);
            RedisKeyExtension._defaultKey = serviceConfig.RedisDefaultKey;
            RedisHelper.Initialization(csredis);

            //添加swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(serviceConfig.Name.ToLower(), new Info
                {
                    Title = serviceConfig.DisplayName,
                    Version = serviceConfig.Version
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

                string filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, serviceConfig.XmlName);
                options.IncludeXmlComments(filePath);
            });
        }

        public static void UseMicroService(this IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime, SysConfig serviceInfo)
        {
            new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            string serviceId = " " + serviceInfo.Name + "-" + serviceInfo.LocalAddress.Replace(':', '-');
            string address = serviceInfo.LocalAddress.Split(new char[] { ':' }, StringSplitOptions.None)[0];
            string s = serviceInfo.LocalAddress.Split(new char[] { ':' }, StringSplitOptions.None)[1];
            //using (ConsulClient consulClient = new ConsulClient(delegate (ConsulClientConfiguration x)
            //{
            //    x.Address = new Uri(serviceInfo.ServiceDiscoveryAddress);
            //}))
            //{
            //    AgentServiceRegistration agentServiceRegistration = new AgentServiceRegistration
            //    {
            //        Address = address,
            //        Port = int.Parse(s),
            //        ID = serviceId,
            //        Name = serviceInfo.Name,
            //        Check = new AgentServiceCheck
            //        {
            //            DeregisterCriticalServiceAfter = new TimeSpan?(TimeSpan.FromSeconds(5.0)),
            //            HTTP = "http://" + serviceInfo.LocalAddress + "/health",
            //            Interval = new TimeSpan?(TimeSpan.FromSeconds(2.0)),
            //            Timeout = new TimeSpan?(TimeSpan.FromSeconds(1.0))
            //        }
            //    };
            //    agentServiceRegistration.Tags = new string[]
            //    {
            //        serviceInfo.DisplayName
            //    };
            //    consulClient.Agent.ServiceRegister(agentServiceRegistration, default(CancellationToken)).Wait();
            //}
            //Action<ConsulClientConfiguration> consul = null;
            //lifetime.ApplicationStopped.Register(delegate ()
            //{
            //    Action<ConsulClientConfiguration> configOverride;
            //    if ((configOverride = consul) == null)
            //    {
            //        configOverride = (consul = delegate (ConsulClientConfiguration x)
            //        {
            //            x.Address = new Uri(serviceInfo.ServiceDiscoveryAddress);
            //        });
            //    }
            //    using (ConsulClient consulClient2 = new ConsulClient(configOverride))
            //    {
            //        consulClient2.Agent.ServiceDeregister(serviceId, default(CancellationToken)).Wait();
            //    } 
            //});
            //app.Map("/health", delegate (IApplicationBuilder ab)
            //{
            //    ab.Run(async delegate (HttpContext context)
            //    {
            //        await context.Response.WriteAsync("ok", default(CancellationToken));
            //    });
            //});

            //if (!env.IsProduction())
            //{
            app.UseSwagger(delegate (SwaggerOptions opt)
            {
                opt.RouteTemplate = "swagger/{documentName}/swagger.json";
            });
            //app.UseSwagger();
            app.UseSwaggerUI(delegate (SwaggerUIOptions opt)
            {
                opt.SwaggerEndpoint("/swagger/" + serviceInfo.Name.ToLower() + "/swagger.json", serviceInfo.DisplayName);
                opt.RoutePrefix = string.Empty;//开启默认swagger/index.html路径
                opt.DocExpansion(DocExpansion.None);
            });
            //}
            app.UseAuthentication();
        }

    }
}
