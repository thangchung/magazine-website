using System;
using Microsoft.EntityFrameworkCore;

namespace Cik.CoreLibs.Domain
{
    public interface IRepository<in TAggregateRootBase, TId>
        where TAggregateRootBase : AggregateRootBase
    {
        DbContext UnitOfWork { get; }
        IObservable<TId> Create(TAggregateRootBase cat);
    }
}