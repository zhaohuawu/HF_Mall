﻿using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using IContainer = Autofac.IContainer;
using Bryan.Common.Interface;
using System;

namespace Bryan.Common.Autofac
{
    public class AutofacConfig
    {
        private static IContainer _container;

        public static IContainer Init(IServiceCollection services, params Assembly[] assemblyArr)
        {
            var builder = new ContainerBuilder();

            // 注册数据库基础操作和工作单元
            services.AddScoped(typeof(IDBManager), typeof(Repository.DBManager));
            services.AddScoped(typeof(IRepository), typeof(Repository.SqlsugarRepository));

            if (services.All(u => u.ServiceType != typeof(IHttpContextAccessor)))
            {
                services.AddScoped(typeof(IHttpContextAccessor), typeof(HttpContextAccessor));
            }

            // 注册app层
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly());

            // 批量注入，继承IAutoFacBase的所有接口
            Type baseType = typeof(IDenpendency);
            if (assemblyArr.Count() > 0)
            {
                builder.RegisterAssemblyTypes(assemblyArr)
                    .Where(type => baseType.IsAssignableFrom(type) && !type.IsAbstract)
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();//InstancePerLifetimeScope 保证对象生命周期基于请求
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
