using System;
using System.Threading.Tasks;
using Cik.CoreLibs.Domain;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Cik.CoreLibs.Bus.Simple
{
    public class SimpleCommandBus : ICommandBus
    {
        private readonly IMediator _mediator;
        private readonly IServiceProvider _provider;

        public SimpleCommandBus(IMediator mediator, IServiceProvider provider)
        {
            Guard.NotNull(mediator);
            Guard.NotNull(provider);
            _mediator = mediator;
            _provider = provider;
        }

        public async Task SendAsync<T>(T command) where T : Command
        {
            // check command validation
            var validators = _provider.GetServices(typeof (ICommandValidator<T>));
            foreach (var validator in validators)
            {
                await ((IValidator<T>) validator).ValidateAndThrowAsync(command);
            }

            // dispatch it 
            await _mediator.PublishAsync(command);
        }
    }
}