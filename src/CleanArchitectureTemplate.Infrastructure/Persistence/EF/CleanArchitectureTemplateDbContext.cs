using System;
#if (!noSampleCode)
using CleanArchitectureTemplate.Infrastructure.Persistence.Postgres.Models;
#endif
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureTemplate.Infrastructure.Persistence.EF
{
    public class CleanArchitectureTemplateDbContext : DbContext
    {
        #if (!noSampleCode)
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<BuyerModel> Buyers { get; set; }
        public DbSet<OrderItemModel> OrderItems { get; set; }
        #endif

        public CleanArchitectureTemplateDbContext(DbContextOptions<CleanArchitectureTemplateDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #if (!noSampleCode)
            modelBuilder.Entity<OrderModel>(model =>
            {
                model.ToTable("Order");
                model.HasKey(x => x.Id);
                model.HasIndex(x => x.Id);
                model.OwnsOne(x => x.ShippingAddress);
                model.Property<DateTime>("CreatedAt");
            });

            modelBuilder.Entity<OrderItemModel>(model =>
            {
                model.ToTable("OrderItem");
                model.HasKey(x => x.Id);
                model.HasIndex(x => x.Id);

                model.HasOne(x => x.Order)
                    .WithMany(x => x.Items)
                    .HasForeignKey(x => x.Id)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<BuyerModel>(model =>
            {
                model.ToTable("Buyer");
                model.HasKey(x => x.Id);
                model.OwnsOne(x => x.Address);
                model.Property<DateTime>("CreatedAt");
            });
            #endif
        }
    }
}
