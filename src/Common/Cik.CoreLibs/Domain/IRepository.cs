using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Cik.CoreLibs.Domain
{
    public interface IRepository<in TAggregateRootBase, TId>
        where TAggregateRootBase : AggregateRootBase
    {
        DbContext UnitOfWork { get; }
        Task<TId> Create(TAggregateRootBase cat);
    }
}