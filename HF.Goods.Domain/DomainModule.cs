using HF.Goods.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace HF.Goods.Domain
{
    [DependsOn(typeof(DomainSharedModule))]
    public class DomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            // 调用基类配置
            base.ConfigureServices(context);

            // 虚拟文件系统
            Configure<VirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<DomainModule>();
            });

            // 本地化
            Configure((AbpLocalizationOptions options) =>
            {
                options.Resources.Get<Shared.DefaultResource>().AddVirtualJson("/Localization/Default"); // 注：这里的斜杠是正确的，不需要改为Path.DirectorySeparatorChar
            });
        }
    }
}
