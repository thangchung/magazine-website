using Cik.Services.CategoryService.Model;
using Microsoft.EntityFrameworkCore;

namespace Cik.Services.CategoryService.Repository
{
    public class CategoryDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./category.db");
        }
    }
}