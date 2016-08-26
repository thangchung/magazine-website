using System.Threading.Tasks;
using Cik.CoreLibs.Domain;

namespace Cik.CoreLibs.Bus.Amqp
{
    public abstract class AmqpCommandHandlerBase<TCommand> : ICommandHandler
         where TCommand : Command
    {
        public abstract Task HandleAsync(TCommand message);
    }
}