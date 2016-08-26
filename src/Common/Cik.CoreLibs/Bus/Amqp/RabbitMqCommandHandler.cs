using System;
using Cik.CoreLibs.Domain;

namespace Cik.CoreLibs.Bus.Amqp
{
    public abstract class RabbitMqCommandHandler<TCommand> : AmqpCommandHandlerBase<TCommand>
        where TCommand : Command
    {
        protected RabbitMqCommandHandler(IServiceProvider serviceProvider)
        {
            Guard.NotNull(serviceProvider);
        }
    }
}