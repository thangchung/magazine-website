using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Cik.CoreLibs.Domain
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public UnitOfWork(DbContext context)
        {
            Guard.NotNull(context);
            _context = context;
        }

        public Task<int> SaveChanges(CancellationToken calCancellationToken = default(CancellationToken))
        {
            return _context.SaveChangesAsync(calCancellationToken);
        }
    }
}