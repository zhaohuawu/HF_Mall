using System;
using Autofac.Extensions.DependencyInjection;
using Bryan.Common.Autofac;
using Bryan.Common.Extension;
using Bryan.MicroService.Jwt;
using Bryan.Common.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Bryan.MicroService;

namespace HF.Goods
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        private SysConfig config;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="env"></param>
        /// <param name="logger"></param>
        public Startup(IConfiguration configuration, IHostingEnvironment env
            , ILogger<Startup> logger)
        {
            Configuration = configuration;
            Env = env;
            _logger = logger;
            config = new SysConfig();
            config.Name = "hfgoods";
            config.DisplayName = "HF商品服务";
            config.Version = "1.0";
            config.XmlName = "HF.GoodsService.xml"; //当前项目 ->属性->生成->输出->勾选XML文档文件,并将XML文件名赋值在这.  
            //宿主机物理网卡地址
            config.LocalAddress = HttpContextExtension.GetLocalIP() + ":" + Configuration.GetSection("ServiceAddress").Value.Split(':')[1];
            //服务发现地址
            config.ServiceDiscoveryAddress = Configuration.GetSection("DCAddress").Value;
            config.RedisConnectionString = Configuration.GetConnectionString("Redis_Hfmall");
            config.RedisDefaultKey = Configuration.GetConnectionString("RedisDafaultKey");
            config.JwtSettings = Configuration.GetSection("JwtSettings").Get<JwtSettings>();
        }
        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }
        private IHostingEnvironment Env { get; }
        private ILogger _logger;

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //注册数据库服务
            DBManager.ConnectionString = Configuration.GetConnectionString("mysql_hfmall");
            DBManager.isLocal = true;
            _logger.LogWarning($"连接字符串串:{DBManager.ConnectionString}");

            services.AddService(config);

            //autofac 注入
            return new AutofacServiceProvider(AutofacConfig.Init(services));
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="lifetime"></param>
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
            app.UseMvc();
        }
    }
}
