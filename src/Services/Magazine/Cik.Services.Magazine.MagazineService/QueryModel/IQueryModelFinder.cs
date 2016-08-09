using System;
using Cik.CoreLibs.Model;
using Cik.Services.Magazine.MagazineService.Model;

namespace Cik.Services.Magazine.MagazineService.QueryModel
{
    public interface IQueryModelFinder<out TDto> where TDto : DtoBase
    {
        IObservable<TDto> FindItemStream(Guid id);
        IObservable<TDto> QueryItemStream();
    }
}