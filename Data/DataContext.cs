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
        public DbSet<Product> Products => Set<Product>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Factory> Factories => Set<Factory>();
        public DbSet<Material> Materials => Set<Material>();

    }
}