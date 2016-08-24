using System.Linq;
using System.Reflection;
using Cik.CoreLibs;
using Cik.CoreLibs.Extensions;
using Cik.Services.Magazine.MagazineService.Api.Category.Entities;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace Cik.Services.Magazine.MagazineService.Infrastruture
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
            modelBuilder.HasPostgresExtension("uuid-ossp");
            modelBuilder.AddEntityConfigurationsFromAssembly(GetType().GetTypeInfo().Assembly);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            var entries = ChangeTracker.Entries<Category>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreatedDate").CurrentValue = Constants.CreatedDate;
                    entry.Property("CreatedBy").CurrentValue = Constants.CreatedUser;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Property("ModifiedDate").CurrentValue = Constants.CreatedDate;
                    entry.Property("ModifiedBy").CurrentValue = Constants.CreatedUser;
                }
            }

            return base.SaveChanges();
        }
    }
}