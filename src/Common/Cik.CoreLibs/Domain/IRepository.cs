using System;

namespace Cik.CoreLibs.Domain
{
    public interface IRepository<TAggregateRootBase, TId>
        where TAggregateRootBase : AggregateRootBase
    {
        IObservable<TId> Create(TAggregateRootBase entity);
        IObservable<TAggregateRootBase> GetByKey(TId id);
    }
}