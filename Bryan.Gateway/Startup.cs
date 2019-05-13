using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bryan.Common;
using Bryan.Gateway.Middleware;
using Bryan.Gateway.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Swashbuckle.AspNetCore.SwaggerUI;

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
            services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));

            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("ApiGateway", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "网关服务",
                    Version = "v1"
                });
            });

            services.AddOcelot(Configuration).AddConsul();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<JwtMiddleware>();

            if (!env.IsProduction())
            {
                var servicesName = Configuration.GetSection("ServicesName").Value.Split(',');
                app.UseSwagger()
                    .UseSwaggerUI(opt =>
                    {
                        foreach (var m in servicesName)
                        {
                            opt.SwaggerEndpoint($"/{m}/swagger.json", m);
                            opt.DocExpansion(DocExpansion.None);
                        }
                    });

            }

            app.UseOcelot().Wait();
        }

    }
}
