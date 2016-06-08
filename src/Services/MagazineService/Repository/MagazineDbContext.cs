using Cik.Services.MagazineService.Model;
using Microsoft.EntityFrameworkCore;

namespace Cik.Services.MagazineService.Repository
{
    public class MagazineDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // TODO: temporary to use it, will change to PostgeSQL soon
            optionsBuilder.UseSqlite("Filename=./magazine.db");
        }
    }
}