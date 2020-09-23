using System;
using CleanArchitectureTemplate.Core.Aggregates;
using CleanArchitectureTemplate.Infrastructure.Persistence.Postgres.Models;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureTemplate.Infrastructure.Persistence.EF
{
    public class CleanArchitectureTemplateDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<BuyerModel> Buyers { get; set; }
        public DbSet<OrderItemModel> OrderItems { get; set; }

        public CleanArchitectureTemplateDbContext(DbContextOptions<CleanArchitectureTemplateDbContext> options) 
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TEMP SOLUTION
            modelBuilder.Entity<OrderModel>(model =>
            {
                model.ToTable("Order");
                model.HasKey(x => x.Id);
                model.OwnsOne(x => x.Address);
                model.Property<DateTime>("CreatedAt");
            });

            modelBuilder.Entity<OrderItemModel>(model =>
            {
                model.ToTable("OrderItem");
                model.HasKey(x => x.Id);
            });

            modelBuilder.Entity<BuyerModel>(model =>
            {
                model.ToTable("Buyer");
                model.HasKey(x => x.Id);
                model.OwnsOne(x => x.Address);
                model.Property<DateTime>("CreatedAt");
            });
        }
    }
}
