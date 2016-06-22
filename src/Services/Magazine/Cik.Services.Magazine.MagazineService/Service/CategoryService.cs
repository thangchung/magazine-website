using System;
using Cik.Domain;
using Cik.Services.Magazine.MagazineService.Model;
using Cik.Services.Magazine.MagazineService.Model.ViewModel;

namespace Cik.Services.Magazine.MagazineService.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category, Guid> _categoryRepository;

        public CategoryService(IRepository<Category, Guid> repo)
        {
            _categoryRepository = repo;
        }

        /*public IObservable<CategoryViewModel> GetAll()
        {
            var categories = _categoryRepository.GetAll();
            return categories.Select(x => new CategoryViewModel
            {
                Name = x.Name
                // CreatedBy = x.CreatedBy,
                // CreatedDate = x.CreatedDate
            });
        }

        public IObservable<Guid> Create(Category cat)
        {
            var idObservable = _categoryRepository.Create(cat);
            _categoryRepository.UnitOfWork.SaveChanges();
            return idObservable;
        } */
    }
}