using EFWebApplication.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFWebApplication
{
    public class AppDbContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = 202-00;Database =Northwind ;User Id=sa;Password = 1234;");
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Product> Products{ get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
