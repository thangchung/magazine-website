using Cik.Data.Entity.Abstraction;
using System;

namespace Cik.Data.Abstraction
{
    public interface IRepository<T> where T : IEntity
    {
        IObservable<T> GetAll();
    }
}