using Cik.CoreLibs.Domain;

namespace Cik.Services.Magazine.MagazineService.Model.Entity
{
    public class Category : AggregateRootBase
    {
        public string Name { get; set; }
    }
}