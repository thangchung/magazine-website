using System;
using Cik.CoreLibs.Model;

namespace Cik.Services.Magazine.MagazineService.Query
{
    public interface IQueryModelFinder<out TDto> where TDto : DtoBase
    {
        IObservable<TDto> FindItemStream(Guid id);
        IObservable<TDto> QueryItemStream();
    }
}