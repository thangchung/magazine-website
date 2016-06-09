using System;
using Microsoft.EntityFrameworkCore;

namespace Cik.Domain
{
    public interface IRepository<AggregateRootBase, out TId>
    {
        DbContext UnitOfWork { get; }
        IObservable<AggregateRootBase> GetAll();
        IObservable<TId> Create(AggregateRootBase cat);
    }
}