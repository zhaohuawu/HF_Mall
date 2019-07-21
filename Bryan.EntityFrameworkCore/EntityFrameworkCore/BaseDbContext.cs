using Bryan.Domain.Sys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Bryan.EntityFrameworkCore.EntityFrameworkCore
{
    [ConnectionStringName("Default")]
    public class BaseDbContext : AbpDbContext<BaseDbContext>
    {
        #region Sys
        public DbSet<SysUser> SysUser { get; set; }

        #endregion

        public BaseDbContext(DbContextOptions<BaseDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            ///* Configure the shared tables (with included modules) here */

            //builder.Entity<AppUser>(b =>
            //{
            //    b.ToTable("AbpUsers"); //Sharing the same table "AbpUsers" with the IdentityUser

            //    b.ConfigureFullAudited();
            //    b.ConfigureExtraProperties();
            //    b.ConfigureConcurrencyStamp();
            //    b.ConfigureAbpUser();

            //    //Moved customization to a method so we can share it with the BookStoreMigrationsDbContext class
            //    b.ConfigureCustomUserProperties();
            //});

            ///* Configure your own tables/entities inside the ConfigureBookStore method */

            //builder.ConfigureBookStore();
        }
    }
}
