using Cik.Domain;

namespace Cik.Services.Magazine.MagazineService.Model
{
  public class Category : AggregateRootBase
  {
    public string Name { get; set; }
  }
}