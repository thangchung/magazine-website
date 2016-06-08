using System;
using System.Reactive.Linq;
using Cik.Services.MagazineService.Model;
using Microsoft.EntityFrameworkCore;
using Cik.Data.Abstraction;

namespace Cik.Services.MagazineService.Repository
{
    public class CategoryRepository : IRepository<Category, Guid>
    {
        private readonly MagazineDbContext _dbContext;

        public CategoryRepository(MagazineDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public DbContext UnitOfWork => _dbContext;

        public IObservable<Category> GetAll()
        {
            return _dbContext.Categories.ToObservable();
        }

        public IObservable<Guid> Create(Category cat)
        {
            cat.CreatedDate = DateTime.UtcNow;
            cat.CreatedBy = "thangchung";
            _dbContext.Categories.Add(cat);
            return Observable.Return(cat.Id);
        }
    }
}