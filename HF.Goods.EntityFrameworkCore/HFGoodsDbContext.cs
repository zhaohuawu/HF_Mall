using HF.Goods.Domain.DomainModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.EntityFrameworkCore;

namespace HF.Goods.EntityFrameworkCore
{
    public abstract class HFGoodsDbContext : DbContext
    {
        public HFGoodsDbContext(DbContextOptions<HFGoodsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

        }
        public DbSet<Gd_Goods> Goods { get; set; }
        public DbSet<Gd_GoodsActivity> GoodsActivity { get; set; }
        public DbSet<Gd_GoodsCategory> GoodsCategory { get; set; }
        public DbSet<Gd_GoodsImg> GoodsImg { get; set; }
        public DbSet<Gd_GoodsInfo> GoodsInfo { get; set; }
        public DbSet<Gd_GoodsSpecs> GoodsSpecs { get; set; }
    }
}
