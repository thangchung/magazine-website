using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Cik.CoreLibs.Bus.Amqp
{
    public class RabbitMqPublisher : DisposableObject, ICommandBus, IEventBus
    {
        private readonly IModel _channel;
        private readonly IConnection _connection;
        private readonly string _exchangeName;
        private readonly IServiceProvider _provider;
        private bool _disposed;

        public RabbitMqPublisher(IServiceProvider provider, string uri, string exchangeName)
        {
            Guard.NotNull(provider);
            Guard.NotNullOrEmpty(uri);
            Guard.NotNullOrEmpty(exchangeName);

            _provider = provider;
            _exchangeName = exchangeName;
            var factory = new ConnectionFactory {Uri = uri};
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public IObservable<Unit> Send<TCommand>(TCommand command) where TCommand : Command
        {
            return Observable.Start(() =>
            {
                // check command validation
                foreach (var validator in _provider.GetServices(typeof (ICommandValidator<TCommand>)))
                {
                    ((IValidator<TCommand>) validator).ValidateAndThrow(command);
                }

                // send to queue
                _channel.ExchangeDeclare(_exchangeName, "fanout");
                var json = JsonConvert.SerializeObject(
                    command,
                    Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    });
                var bytes = Encoding.UTF8.GetBytes(json);
                _channel.BasicPublish(_exchangeName, "", null, bytes);
            });
        }

        public IObservable<Unit> Publish<TEvent>(TEvent @event) where TEvent : Event
        {
            throw new NotImplementedException();
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (_disposed) return;
            _channel.Dispose();
            _connection.Dispose();
            _disposed = true;
        }
    }
}