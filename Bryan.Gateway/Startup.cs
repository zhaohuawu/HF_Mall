using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bryan.Gateway.Models;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Bryan.Gateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("ApiGateway", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "网关服务",
                    Version = "v1"
                });
            });

            services.AddOcelot(Configuration);
            // 服务注册
            //services.Configure<ServiceRegisterOptions>(Configuration.GetSection("ServiceRegister"));
            //services.AddSingleton<IConsulClient>(x => new ConsulClient(cfg =>
            //{
            //    var serviceConfig = x.GetRequiredService<IOptions<ServiceRegisterOptions>>().Value;
            //    if (!string.IsNullOrEmpty(serviceConfig.Register.HttpEndpoint))
            //    {
            //        cfg.Address = new Uri(serviceConfig.Register.HttpEndpoint);
            //    }
            //}));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseMvc();
            var apis = new List<string> { "base", "hfgoods" };
            app.UseSwagger()
                .UseSwaggerUI(opt =>
                {
                    apis.ForEach(m =>
                    {
                        opt.SwaggerEndpoint($"/{m}/swagger.json", m);
                    });
                });


            //app.UseOcelot().Wait();
        }


    }
}
