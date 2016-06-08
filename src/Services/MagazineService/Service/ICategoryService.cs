using System;
using Cik.Services.MagazineService.Model;
using Cik.Services.MagazineService.Model.ViewModel;

namespace Cik.Services.MagazineService.Service
{
    public interface ICategoryService
    {
        IObservable<CategoryViewModel> GetAll();
        IObservable<Guid> Create(Category cat);
    }
}