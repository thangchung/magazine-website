using Cik.Services.Magazine.MagazineService.Model;
using Microsoft.EntityFrameworkCore;

namespace Cik.Services.Magazine.MagazineService.Repository
{
    public class MagazineDbContext : DbContext
    {
        public MagazineDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().ToTable("Categories");
        }
    }
}