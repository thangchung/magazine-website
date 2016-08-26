using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Cik.CoreLibs.Bus
{
    public sealed class CommandConsumer : DisposableObject, ICommandConsumer
    {
        private bool _disposed;

        public CommandConsumer(IMessageSubscriber subscriber, IEnumerable<ICommandHandler> commandHandlers)
        {
            Subscriber = subscriber;
            CommandHandlers = commandHandlers;
            subscriber.MessageReceived += async (sender, e) =>
            {
                if (CommandHandlers != null)
                {
                    foreach (var handler in CommandHandlers)
                    {
                        var handlerType = handler.GetType();
                        var messageType = e.Message.GetType();
                        var methodInfoQuery =
                            from method in handlerType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                            let parameters = method.GetParameters()
                            where method.Name == "HandleAsync" &&
                                  method.ReturnType == typeof (Task) &&
                                  parameters.Length == 1 &&
                                  parameters[0].ParameterType == messageType
                            select method;
                        var methodInfo = methodInfoQuery.FirstOrDefault();
                        if (methodInfo != null)
                        {
                            await (Task) methodInfo.Invoke(handler, new[] {e.Message});
                        }
                    }
                }
            };
        }

        public IEnumerable<ICommandHandler> CommandHandlers { get; }

        public IMessageSubscriber Subscriber { get; }

        protected override void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (_disposed) return;
            Subscriber.Dispose();
            _disposed = true;
        }
    }
}