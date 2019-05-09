using Bryan.Gateway.Models;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bryan.Gateway.Common
{
    public static class ConsulExtensions
    {
        public static void ConsulApp(this IApplicationBuilder app,
            IHostingEnvironment env,
            IApplicationLifetime lifetime,
            IOptions<ServiceRegisterOptions> registerOptions,
            IConsulClient consul)
        {
            //注册服务
            lifetime.ApplicationStarted.Register(() =>
            {
                RegisterService(app, lifetime, registerOptions, consul);
            });

            //停止的时候移除服务
            lifetime.ApplicationStopped.Register(() => {
                
            });
        }

        //中间件 注册服务
        private static void RegisterService(IApplicationBuilder app,
            IApplicationLifetime lifetime,
            IOptions<ServiceRegisterOptions> registerOptions,
            IConsulClient consul)
        {
            var features = app.Properties["server.Features"] as FeatureCollection;
            var addresses = features.Get<IServerAddressesFeature>().Addresses.Select(p => new Uri(p));
            foreach (var address in addresses)
            {
                var serviceId = $"{registerOptions.Value.ServiceName}_{address.Host}:{address.Port}";
                var httpCheck = new AgentServiceCheck()
                {
                    DeregisterCriticalServiceAfter = TimeSpan.FromMinutes(1),
                    Interval = TimeSpan.FromSeconds(30),
                    HTTP = new Uri(address, "healthcheck").OriginalString
                };

                var registration = new AgentServiceRegistration()
                {
                    Checks = new[] { httpCheck },
                    Address = address.Host,
                    ID = serviceId,
                    Name = registerOptions.Value.ServiceName,
                    Port = address.Port
                };
                consul.Agent.ServiceRegister(registration).GetAwaiter().GetResult();
                lifetime.ApplicationStopping.Register(() =>
                {
                    consul.Agent.ServiceDeregister(serviceId).GetAwaiter().GetResult();
                });
            }
        }
    }
}
