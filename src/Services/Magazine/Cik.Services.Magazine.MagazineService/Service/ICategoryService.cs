using System;
using Cik.Domain;
using Cik.Services.Magazine.MagazineService.Model;
using Cik.Services.Magazine.MagazineService.Model.ViewModel;

namespace Cik.Services.Magazine.MagazineService.Service
{
    public interface ICategoryService : IDomainService
    {
        IObservable<CategoryViewModel> GetAll();
        IObservable<Guid> Create(Category cat);
    }
}