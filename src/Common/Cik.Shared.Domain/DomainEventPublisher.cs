using System;

namespace Cik.Shared.Domain
{
    public class DomainEventPublisher : IDomainEventPublisher
    {
        public void Publish<T>(T @event) where T : Event
        {
            throw new NotImplementedException();
        }
    }
}