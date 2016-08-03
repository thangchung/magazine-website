using System;
using Cik.CoreLibs.Domain;
using Cik.Services.Magazine.MagazineService.Model;
using System.Reactive.Linq;

namespace Cik.Services.Magazine.MagazineService.QueryModel
{
    public class CategoryQueryModelFinder : IQueryModelFinder<CategoryDto>
    {
        private readonly MagazineDbContext _dbContext;

        public CategoryQueryModelFinder(MagazineDbContext dbContext)
        {
            Guard.NotNull(dbContext);
            _dbContext = dbContext;
        }

        public IObservable<CategoryDto> FindItemStream(Guid id)
        {
            return GetCategoryStream().Where(x=>x.Id == id);
        }

        public IObservable<CategoryDto> QueryItemStream()
        {
            return GetCategoryStream();
        }

        private IObservable<CategoryDto> GetCategoryStream()
        {
            return _dbContext
                .Categories
                .ToObservable()
                .Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name
                });
        }

        #region "async version"

        /*public async Task<CategoryDto> Find(Guid id)
        {
            var dbItem = _dbContext
                .Categories
                .Where(x => x.Id == id)
                .Select(x =>
                    new CategoryDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        CreatedBy = x.CreatedBy,
                        CreatedDate = x.CreatedDate
                    })
                .FirstAsync();
            return await dbItem;
        }

        public async Task<List<CategoryDto>> Query()
        {
            var categories = _dbContext
                .Categories
                .Select(x =>
                    new CategoryDto
                    {
                        Id = x.Id,
                        Name = x.Name
                    })
                .ToListAsync();
            return await categories;
        } */

        #endregion
    }
}