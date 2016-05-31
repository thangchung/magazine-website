using System;
using System.Reactive.Linq;
using Cik.Data.Abstraction;
using Cik.Services.CategoryService.Model;
using Microsoft.EntityFrameworkCore;

namespace Cik.Services.CategoryService.Repository
{
    public class CategoryRepository : IRepository<Category, Guid>
    {
        private readonly CategoryDbContext _dbContext;

        public CategoryRepository(CategoryDbContext dbContext)
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