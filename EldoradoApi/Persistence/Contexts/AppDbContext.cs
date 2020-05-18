using System;
using EldoradoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EldoradoApi.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Order>().ToTable("Orders");
            builder.Entity<Order>().HasKey(p => p.Id);
            builder.Entity<Order>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Entity<Order>().HasData
            (
                new Order
                {
                    Id = 100,
                    UserId = 100,
                    SellerId = 101,
                    CreationDate = DateTime.UtcNow,
                    ExpiryDate = DateTime.UtcNow.AddHours(2),
                    Paid=false,
                    Completed = false,
                },
                new Order
                {
                    Id = 101,
                    UserId = 101,
                    SellerId = 100,
                    CreationDate = DateTime.UtcNow,
                    ExpiryDate = DateTime.UtcNow.AddHours(2),
                    Paid = false,
                    Completed = false,
                }
            ); ;
        }
    }
}
