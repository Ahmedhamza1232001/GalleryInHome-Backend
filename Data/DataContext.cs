using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Proj.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Material>().HasData(
                new Material { Id = 1, Name = "Wood" },
                new Material { Id = 2, Name = "Glass" },
                new Material { Id = 3, Name = "Fiber" }
            );

            modelBuilder.Entity<User>()
                .HasMany(u => u.Products)
                .WithMany(u => u.Users)
                .UsingEntity<Favorite>(
                j => j
                    .HasOne(f => f.Product)
                    .WithMany(p => p.Favorites)
                    .HasForeignKey(f => f.ProductId),
                j => j
                    .HasOne(f => f.User)
                    .WithMany(u => u.Favorites)
                    .HasForeignKey(f => f.UserId),
                j => j.HasKey(t => new { t.UserId, t.ProductId })
                );



        }
        public DbSet<Product> Products => Set<Product>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Factory> Factories => Set<Factory>();
        public DbSet<Material> Materials => Set<Material>();

    }
}