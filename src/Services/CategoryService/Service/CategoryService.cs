using System;
using System.Reactive.Linq;
using Cik.Data.Abstraction;
using Cik.Service;
using Cik.Services.CategoryService.Model;
using Cik.Services.CategoryService.Model.ViewModel;

namespace Cik.Services.CategoryService.Service
{
    public class CategoryService : ServiceBase<Category, Guid>, ICategoryService
    {
        public CategoryService(IRepository<Category, Guid> repo)
            : base(repo)
        {
        }

        public IObservable<CategoryViewModel> GetAll()
        {
            var categories = BaseEntityRepository.GetAll();
            return categories.Select(x => new CategoryViewModel
            {
                Name = x.Name,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate
            });
        }

        public IObservable<Guid> Create(Category cat)
        {
            var idObservable = BaseEntityRepository.Create(cat);
            BaseEntityRepository.UnitOfWork.SaveChanges();
            return idObservable;
        }
    }
}