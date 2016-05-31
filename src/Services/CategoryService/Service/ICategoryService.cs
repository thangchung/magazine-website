using System;
using Cik.Services.CategoryService.Model;
using Cik.Services.CategoryService.Model.ViewModel;

namespace Cik.Services.CategoryService.Service
{
    public interface ICategoryService
    {
        IObservable<CategoryViewModel> GetAll();
        IObservable<Guid> Create(Category cat);
    }
}