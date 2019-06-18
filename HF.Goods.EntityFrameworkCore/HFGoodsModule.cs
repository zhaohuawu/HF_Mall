using HF.Goods.Domain;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Modularity;

namespace HF.Goods.EntityFrameworkCore
{
    [DependsOn(typeof(DomainModule))]
    public class HFGoodsModule: AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            // 调用基类配置
            base.ConfigureServices(context);

            // 注册仓储
            //context.Services.AddAbpDbContext<HFGoodsDbContext>(options =>
            //{
            //    options.AddDefaultRepositories(includeAllEntities: true);
            //});
        }
    }
}
