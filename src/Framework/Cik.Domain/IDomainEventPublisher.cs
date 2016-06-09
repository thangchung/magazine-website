namespace Cik.Domain
{
    public interface IDomainEventPublisher
    {
        void Publish(Event @event);
    }
}