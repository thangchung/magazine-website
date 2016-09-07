using Cik.CoreLibs.Bus;

namespace Cik.Services.Magazine.MagazineService.Api.Category.Events
{
    public class CategoryPublished : Event
    {
        public bool IsPublished { get; set; }
    }
}