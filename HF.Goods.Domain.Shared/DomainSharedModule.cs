using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace HF.Goods.Domain.Shared
{
    public class DomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            base.ConfigureServices(context);

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources.Add<DefaultResource>("Default");
            });
        }
    }
}
