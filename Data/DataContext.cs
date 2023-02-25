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
        }
        public DbSet<Product> Products => Set<Product>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Factory> Factories => Set<Factory>();
        public DbSet<Material> Materials => Set<Material>();

    }
}