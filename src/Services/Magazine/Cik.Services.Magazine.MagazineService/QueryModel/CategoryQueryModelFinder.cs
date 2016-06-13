using System;
using System.Reactive.Linq;
using Cik.Domain;
using Cik.Services.Magazine.MagazineService.Model;
using Cik.Services.Magazine.MagazineService.Repository;
using Microsoft.EntityFrameworkCore;

namespace Cik.Services.Magazine.MagazineService.QueryModel
{
    public class CategoryQueryModelFinder : IQueryModelFinder
    {
        private readonly MagazineDbContext _dbContext;

        public CategoryQueryModelFinder(MagazineDbContext dbContext)
        {
            Guard.NotNull(dbContext);

            _dbContext = dbContext;
        }

        public IObservable<CategoryDto> Find(Guid categoryId)
        {
            var dbItem = _dbContext
                .Categories
                .FromSql(@"SELECT Id, Name FROM Categories WHERE Id='p0'", categoryId);

            return dbItem
                .ToObservable()
                .Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .FirstAsync();
            // return Observable.Return(new CategoryDto());
        }

        public IObservable<CategoryDto> Query()
        {
            var categories = _dbContext.Categories;
            return categories
                .ToObservable()
                .Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name
                });
        }
    }
}