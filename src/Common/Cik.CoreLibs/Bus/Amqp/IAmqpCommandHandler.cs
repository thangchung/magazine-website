using System;
using System.Reactive;

namespace Cik.CoreLibs.Bus.Amqp
{
    public interface IAmqpCommandHandler<in TCommand> : ICommandHandler
        where TCommand : Command
    {
        IObservable<Unit> Handle(TCommand message);
    }
}