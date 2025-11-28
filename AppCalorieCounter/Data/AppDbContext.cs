using AppCalorieCounter.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCalorieCounter.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=DB_Product.db");
        }
        public static void EnsureDatabaseCreated()
        {
            using (var context = new AppDbContext())
            {
                context.Database.EnsureCreated();
            }

        }
    }
}
