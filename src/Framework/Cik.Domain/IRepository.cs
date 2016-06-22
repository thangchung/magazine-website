using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Cik.Domain
{
    public interface IRepository<TAggregateRootBase, TId>
        where TAggregateRootBase : AggregateRootBase
    {
        DbContext UnitOfWork { get; }
        Task<List<TAggregateRootBase>> GetAll();
        Task<TId> Create(TAggregateRootBase cat);
    }
}