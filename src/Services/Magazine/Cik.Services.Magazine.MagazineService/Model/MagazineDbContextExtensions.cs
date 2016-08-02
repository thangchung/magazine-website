using System;
using System.Threading.Tasks;
using System.Linq;
using Cik.CoreLibs.Domain;

namespace Cik.Services.Magazine.MagazineService.Model
{
    public static class MagazineDbContextExtensions
    {
        public static async Task EnsureSeedDataAsync(this MagazineDbContext dbContext)
        {
            if (!dbContext.AllMigrationsApplied()) return;
            if (!dbContext.Categories.Any())
            {
                for (var i = 1; i < 1000; i++)
                {
                    dbContext.Categories.Add(
                        new Category
                        {
                            Name = $"category {i}",
                            Id = Guid.NewGuid(),
                            AggregateStatus = AggregateStatus.Active
                        }
                        );
                }
                await dbContext.SaveChangesAsync();
            }
        }
    }
}