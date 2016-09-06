using System;

namespace Cik.CoreLibs.Domain
{
    public interface IRepository<in TAggregateRootBase, out TId>
        where TAggregateRootBase : AggregateRootBase
    {
        IObservable<TId> Create(TAggregateRootBase entity);
    }
}