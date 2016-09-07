using System;
using System.Reactive;
using Cik.CoreLibs.Domain;

namespace Cik.CoreLibs.Bus.Amqp
{
    public abstract class AmqpEventHandlerBase<TEvent> : IAmqpEventHandler<TEvent>
        where TEvent : Event
    {
        protected IUnitOfWork UnitOfWork;

        protected AmqpEventHandlerBase(IUnitOfWork uow)
        {
            Guard.NotNull(uow);
            UnitOfWork = uow;
        }

        public abstract IObservable<Unit> Handle(TEvent message);
    }
}