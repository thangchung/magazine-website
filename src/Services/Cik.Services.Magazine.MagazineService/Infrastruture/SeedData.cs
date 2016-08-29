using System;
using System.Linq;
using System.Threading.Tasks;
using Cik.CoreLibs.Domain;
using Cik.Services.Magazine.MagazineService.Api.Category.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Cik.Services.Magazine.MagazineService.Infrastruture
{
    public class SeedData
    {
        public static async Task InitializeMagazineDatabaseAsync(IServiceProvider serviceProvider,
            bool createUsers = true)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<MagazineDbContext>();
                await db.Database.MigrateAsync();
                using (db)
                {
                    await InsertTestData(db);
                }
            }
        }

        private static async Task InsertTestData(MagazineDbContext dbContext)
        {
            if (!dbContext.Categories.Any())
            {
                for (var i = 1; i < 1000; i++)
                {
                    dbContext.Categories.Add(
                        new Category
                        {
                            Id = Guid.NewGuid(),
                            Name = $"category {i}",
                            AggregateStatus = AggregateStatus.Active,
                            CreatedBy = "admin",
                            CreatedDate = DateTimeOffset.UtcNow
                        });
                }
                await dbContext.SaveChangesAsync();
            }
        }
    }
}