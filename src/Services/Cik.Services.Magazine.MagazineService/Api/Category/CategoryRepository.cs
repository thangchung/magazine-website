using System;
using System.Reactive.Linq;
using Cik.CoreLibs;
using Cik.CoreLibs.Domain;
using Cik.Services.Magazine.MagazineService.Infrastruture;

namespace Cik.Services.Magazine.MagazineService.Api.Category
{
    public class CategoryRepository : IRepository<Entities.Category, Guid>
    {
        private readonly MagazineDbContext _dbContext;

        public CategoryRepository(MagazineDbContext dbContext)
        {
            Guard.NotNull(dbContext);
            _dbContext = dbContext;
        }

        public IObservable<Guid> Create(Entities.Category cat)
        {
            var retCat = _dbContext.Categories.Add(cat);
            return Observable.Return(retCat.Entity.Id);
        }
    }
}