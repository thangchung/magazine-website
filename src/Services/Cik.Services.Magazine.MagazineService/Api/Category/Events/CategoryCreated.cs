using System;
using Cik.CoreLibs.Bus;

namespace Cik.Services.Magazine.MagazineService.Api.Category.Events
{
    public class CategoryCreated : Event
    {
        public Guid Id { get; set; }
    }
}