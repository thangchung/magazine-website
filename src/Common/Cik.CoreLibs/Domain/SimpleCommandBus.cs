using System.Threading.Tasks;
using MediatR;

namespace Cik.CoreLibs.Domain
{
    public class SimpleCommandBus : ICommandBus
    {
        private readonly IMediator _mediator;

        public SimpleCommandBus(IMediator mediator)
        {
            Guard.NotNull(mediator);
            _mediator = mediator;
        }

        public Task SendAsync<T>(T command) where T : Command
        {
            return _mediator.PublishAsync(command);
        }
    }
}