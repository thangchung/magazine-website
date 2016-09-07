using System;
using System.Reactive.Linq;
using Cik.CoreLibs;
using Cik.CoreLibs.Model;
using Cik.Services.Magazine.MagazineService.Api.Category.Dtos;
using Cik.Services.Magazine.MagazineService.Infrastruture;

namespace Cik.Services.Magazine.MagazineService.Api.Category
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
            return GetCategoryStream().Where(x => x.Id == id);
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
    }
}