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
            //lifetime.ApplicationStopped.Register(() => {
            //    using (ConsulClient consulClient2 = new ConsulClient(x =>
            //    {
            //        x.Address = new Uri(serviceInfo.ServiceDiscoveryAddress);
            //    }))
            //    {
            //        var features = app.Properties["server.Features"] as FeatureCollection;
            //        var addresses = features.Get<IServerAddressesFeature>().Addresses.Select(p => new Uri(p));
            //        var serviceId = $"{registerOptions.Value.ServiceName}_{address.Host}:{address.Port}";
            //        consulClient2.Agent.ServiceDeregister(serviceId, default(CancellationToken)).Wait();
            //    }
            //});
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

        /// <summary>
        /// 服务发现
        /// </summary>
        /// <param name="consulUrl"></param>
        public static void UserConsul(string consulUrl)
        {
            using (ConsulClient consulClient = new ConsulClient(c => c.Address = new Uri(consulUrl)))
            {
                //consulClient.Agent.Services()获取consul中注册的所有的服务
                Dictionary<String, AgentService> services = consulClient.Agent.Services().Result.Response;
                foreach (KeyValuePair<String, AgentService> kv in services)
                {
                    Console.WriteLine($"key={kv.Key},{kv.Value.Address},{kv.Value.ID},{kv.Value.Service},{kv.Value.Port}");
                }
                
                //获取所有服务名字是"apiservice1"所有的服务
                //var agentServices = services.Where(s => s.Value.Service.Equals("apiservice1", StringComparison.CurrentCultureIgnoreCase))
                //   .Select(s => s.Value);
                ////根据当前TickCount对服务器个数取模，“随机”取一个机器出来，避免“轮询”的负载均衡策略需要计数加锁问题
                //var agentService = agentServices.ElementAt(Environment.TickCount % agentServices.Count());
                //Console.WriteLine($"{agentService.Address},{agentService.ID},{agentService.Service},{agentService.Port}");
            }
        }
    }
}
