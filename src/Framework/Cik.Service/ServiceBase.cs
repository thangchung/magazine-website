using Cik.Domain;
using Microsoft.EntityFrameworkCore;

namespace Cik.Service
{
    // TODO: will think about this later
    public abstract class ServiceBase<TEntity, TId> : IService 
        where TEntity : AggregateRootBase
    {
        protected IRepository<TEntity, TId> BaseEntityRepository;
        protected DbContext UnitOfWork { get; }

        protected ServiceBase(IRepository<TEntity, TId> repo)
        {
            BaseEntityRepository = repo;
            UnitOfWork = repo.UnitOfWork;
        }
    }
}