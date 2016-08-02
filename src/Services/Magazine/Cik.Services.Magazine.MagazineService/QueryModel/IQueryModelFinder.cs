using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cik.Services.Magazine.MagazineService.Model;

namespace Cik.Services.Magazine.MagazineService.QueryModel
{
    public interface IQueryModelFinder<TDto> where TDto : DtoBase
    {
        Task<TDto> Find(Guid id);
        Task<List<TDto>> Query();
    }
}