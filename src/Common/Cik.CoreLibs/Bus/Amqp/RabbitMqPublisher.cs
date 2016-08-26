using System;
using System.Text;
using System.Threading.Tasks;
using Cik.CoreLibs.Domain;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Cik.CoreLibs.Bus.Amqp
{
    public class RabbitMqPublisher : DisposableObject, ICommandBus, IEventBus
    {
        private readonly IModel _channel;
        private readonly IConnection _connection;
        private readonly string _exchangeName;
        private bool _disposed;

        public RabbitMqPublisher(string uri, string exchangeName)
        {
            Guard.NotNullOrEmpty(uri);
            Guard.NotNullOrEmpty(exchangeName);

            _exchangeName = exchangeName;
            var factory = new ConnectionFactory {Uri = uri};
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public Task SendAsync<TCommand>(TCommand command) where TCommand : Command
        {
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
            return Task.CompletedTask;
        }

        public Task PublishAsync<TEvent>(TEvent @event) where TEvent : Event
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