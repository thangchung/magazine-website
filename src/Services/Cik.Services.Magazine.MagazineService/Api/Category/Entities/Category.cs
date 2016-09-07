using Cik.CoreLibs.Bus;
using Cik.CoreLibs.Domain;
using Cik.Services.Magazine.MagazineService.Api.Category.Events;

namespace Cik.Services.Magazine.MagazineService.Api.Category.Entities
{
    public class Category : AggregateRootBase
    {
        public Category()
        {
            ApplyEvent(new CategoryCreated());
        }

        public string Name { get; set; }

        public bool IsPublish { get; private set; }

        public void Publish()
        {
            IsPublish = true;
            ApplyEvent(new CategoryPublished {IsPublished = IsPublish});
        }

        [EventHandler]
        private void Handle(CategoryCreated @event)
        {
            Id = @event.Id;
        }
    }
}