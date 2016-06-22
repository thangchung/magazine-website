using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        /*public IObservable<CategoryDto> Find(Guid categoryId)
        {
            var dbItem = _dbContext
                .Categories
                .FromSql("SELECT Id, Name FROM Categories WHERE Id=(@p0)", categoryId);

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
        } */

        public async Task<CategoryDto> Find(Guid categoryId)
        {
            var dbItem = _dbContext
                .Categories
                .FromSql("SELECT Id, Name FROM Categories WHERE Id=(@p0)", categoryId);
            var cat = await dbItem.FirstAsync();
            return new CategoryDto
            {
                Id = cat.Id,
                Name = cat.Name
            };
        }

        public async Task<List<CategoryDto>> Query()
        {
            var categories = _dbContext.Categories;
            return await categories.Select(x => new CategoryDto
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
        }
    }
}