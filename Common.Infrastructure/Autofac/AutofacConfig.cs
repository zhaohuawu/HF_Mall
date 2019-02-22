using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using IContainer = Autofac.IContainer;
using Common.Infrastructure;
using Common.Infrastructure.Interface;

namespace Common.Infrastructure.Autofac
{
    public class AutofacConfig
    {
        private static IContainer _container;


        public static IContainer Init(IServiceCollection services)
        {
            var builder = new ContainerBuilder();

            //注册数据库基础操作和工作单元
            //services.AddScoped(typeof(IUnitWork), typeof(UnitWork));
            services.AddScoped(typeof(ILog), typeof(Log.LogHelper));
            services.AddScoped(typeof(IRepository), typeof(Repository.SqlsugarRepository));
            //如果想使用WebApi SSO授权，请使用下面这种方式

            //注册app层
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly());

            if (services.All(u => u.ServiceType != typeof(IHttpContextAccessor)))
            {
                services.AddScoped(typeof(IHttpContextAccessor), typeof(HttpContextAccessor));
            }

            builder.Populate(services);

            _container = builder.Build();
            return _container;
        }

        /// <summary>
        /// 从容器中获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static T GetFromFac<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
