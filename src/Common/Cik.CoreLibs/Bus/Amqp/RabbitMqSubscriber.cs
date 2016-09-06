using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Cik.CoreLibs.Bus.Amqp
{
    public class RabbitMqSubscriber : DisposableObject, IMessageSubscriber
    {
        private readonly IModel _channel;
        private readonly IConnection _connection;
        private readonly string _exchangeName;
        private readonly string _queueName;
        private bool _disposed;

        public RabbitMqSubscriber(string uri, string exchangeName, string queueName)
        {
            _exchangeName = exchangeName;
            _queueName = queueName;
            var factory = new ConnectionFactory {Uri = uri};
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        public IObservable<Unit> Subscribe()
        {
            return Observable.Start(() =>
            {
                _channel.ExchangeDeclare(_exchangeName, "fanout");
                _channel.QueueDeclare(_queueName, true, false, false, null);
                _channel.QueueBind(_queueName, _exchangeName, "");

                var consumer = new EventingBasicConsumer(_channel);
                consumer.Received += (sender, e) =>
                {
                    var body = e.Body;
                    var json = Encoding.UTF8.GetString(body);
                    var message = JsonConvert.DeserializeObject(
                        json,
                        new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All
                        });
                    OnMessageReceived(new MessageReceivedEventArgs(message));
                    _channel.BasicAck(e.DeliveryTag, false);
                };
                _channel.BasicConsume(_queueName, false, consumer);
            });
        }

        private void OnMessageReceived(MessageReceivedEventArgs e)
        {
            MessageReceived?.Invoke(this, e);
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