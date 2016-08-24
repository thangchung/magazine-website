using System;
using System.Linq;
using System.Threading.Tasks;
using Cik.CoreLibs.Domain;
using Cik.CoreLibs.Extensions;
using Cik.Services.Magazine.MagazineService.Api.Category.Entities;

namespace Cik.Services.Magazine.MagazineService.Infrastruture.Extensions
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