using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductSystem.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                    : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Phone" },
                new Category { Id = 2, Name = "Laptop" },
                new Category { Id = 3, Name = "Watch" },
                new Category { Id = 4, Name = "Earbuds" }
            );
        }
        public DbSet<Product> Products { get; private set; }
        public DbSet<Category> Categories { get; private set; }
        public DbSet<ProductLog> ProductLogs { get; private set; }

    }
}
