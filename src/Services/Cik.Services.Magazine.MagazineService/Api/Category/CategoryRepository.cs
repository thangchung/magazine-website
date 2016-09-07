using System;
using System.Linq;
using System.Reactive.Linq;
using Cik.CoreLibs;
using Cik.CoreLibs.Bus;
using Cik.CoreLibs.Domain;
using Cik.Services.Magazine.MagazineService.Infrastruture;

namespace Cik.Services.Magazine.MagazineService.Api.Category
{
    public class CategoryRepository : IRepository<Entities.Category, Guid>
    {
        private readonly IEventBus _eventBus;
        private readonly MagazineDbContext _dbContext;

        public CategoryRepository(MagazineDbContext dbContext, IEventBus eventBus)
        {
            Guard.NotNull(dbContext);
            Guard.NotNull(eventBus);
            _dbContext = dbContext;
            _eventBus = eventBus;
        }

        public IObservable<Guid> Create(Entities.Category cat)
        {
            var retCat = _dbContext.Categories.Add(cat);
            foreach (var @event in retCat.Entity.UncommittedEvents)
            {
                _eventBus.Publish(@event);
            }
            return Observable.Return(retCat.Entity.Id);
        }

        public IObservable<Entities.Category> GetByKey(Guid id)
        {
            return Observable.Return(
                _dbContext.Categories.FirstOrDefault(
                    x => x.Id == id
                         && x.AggregateStatus == AggregateStatus.Active)
                );
        }
    }
}