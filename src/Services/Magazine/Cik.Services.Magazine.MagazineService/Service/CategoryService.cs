using System;
using System.Reactive.Linq;
using Cik.Domain;
using Cik.Service;
using Cik.Services.Magazine.MagazineService.Model;
using Cik.Services.Magazine.MagazineService.Model.ViewModel;

namespace Cik.Services.Magazine.MagazineService.Service
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
                Name = x.Name
                // CreatedBy = x.CreatedBy,
                // CreatedDate = x.CreatedDate
            });
        }

        public IObservable<Guid> Create(Category cat)
        {
            var idObservable = BaseEntityRepository.Create(cat);
            UnitOfWork.SaveChanges();
            return idObservable;
        }
    }
}