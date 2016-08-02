namespace Cik.CoreLibs.Domain
{
    public interface IDomainEventPublisher
    {
        void Publish<T>(T @event) where T : Event;
    }

    public interface IHandles<in T>
    {
        void Handle(T message);
    }
}