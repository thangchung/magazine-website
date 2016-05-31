using System;
using Microsoft.EntityFrameworkCore;

namespace Cik.Data.Abstraction
{
    public interface IRepository<TEntity, out TId>
    {
        DbContext UnitOfWork { get; }
        IObservable<TEntity> GetAll();
        IObservable<TId> Create(TEntity cat);
    }
}