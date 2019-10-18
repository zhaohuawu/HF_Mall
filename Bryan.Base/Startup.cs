using System;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Bryan.Common.Autofac;
using Bryan.Common.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Bryan.Common;
using Bryan.Base.Models.AppSettings;
using Bryan.MicroService.Jwt;
using Microsoft.Extensions.Logging;
using Bryan.Common.Extension;
using System.Reflection;
using Bryan.MicroService;

namespace Bryan.Base
{
    public class Startup
    {
        private SysConfig config;
        public Startup(IConfiguration configuration, IHostingEnvironment env
            , ILogger<Startup> logger)
        {
            _logger.LogWarning($"连接字符串串:{HttpContextExtension.GetLocalIP()}");
            Configuration = configuration;
            Env = env;
            _logger = logger;
            config = new SysConfig();
            config.Name = "base";
            config.DisplayName = "基础服务";
            config.Version = "1.0";
            config.XmlName = "Bryan.BaseService.xml"; //当前项目 ->属性->生成->输出->勾选XML文档文件,并将XML文件名赋值在这.  
            //宿主机物理网卡地址
            config.LocalAddress = HttpContextExtension.GetLocalIP() + ":" + Configuration.GetSection("ServiceAddress").Value.Split(':')[1];
            //服务发现地址
            //config.ServiceDiscoveryAddress = Configuration.GetSection("DCAddress").Value;
            config.RedisConnectionString = Configuration.GetConnectionString("Redis_Hfmall");
            config.RedisDefaultKey = Configuration.GetConnectionString("RedisDafaultKey");
            config.JwtSettings = Configuration.GetSection("JwtSettings").Get<JwtSettings>();
        }

        public IConfiguration Configuration { get; }
        private IHostingEnvironment Env { get; }
        private ILogger _logger;

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //AppSettings 参数配置
            var appSettingsSection = Configuration.GetSection("AppSettings");
            var appSettings = appSettingsSection.Get<AppSettings>();
            services.Configure<AppSettings>(appSettingsSection);
            services.Configure<UploadSettings>(Configuration.GetSection("Upload"));

            //注册数据库服务
            DBManager.ConnectionString = Configuration.GetConnectionString("mysql_hfmall");
            DBManager.isLocal = appSettings.IsLocal;
            _logger.LogWarning($"连接字符串串:{DBManager.ConnectionString}");

            services.AddService(config);

            // 获取所有相关类库的程序集,通过命名空间和反射获取Assembly
            Assembly[] assemblyArr = { Assembly.Load("Bryan.BaseService") };
            //autofac 注入
            return new AutofacServiceProvider(AutofacConfig.Init(services, assemblyArr));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
