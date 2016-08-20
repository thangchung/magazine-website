using Cik.CoreLibs.Domain;

namespace Cik.Services.Magazine.MagazineService.Features.Category.Entities
{
    public class Category : AggregateRootBase
    {
        public string Name { get; set; }
    }
}