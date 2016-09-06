using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reflection;

namespace Cik.CoreLibs.Bus
{
    public sealed class CommandConsumer : DisposableObject, ICommandConsumer
    {
        private bool _disposed;

        public CommandConsumer(
            IMessageSubscriber subscriber, 
            IEnumerable<ICommandHandler> commandHandlers)
        {
            Subscriber = subscriber;
            CommandHandlers = commandHandlers;
            subscriber.MessageReceived += (sender, e) =>
            {
                if (CommandHandlers == null) return;
                foreach (var handler in CommandHandlers)
                {
                    var handlerType = handler.GetType();
                    var messageType = e.Message.GetType();
                    var methodInfoQuery =
                        from method in handlerType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                        let parameters = method.GetParameters()
                        where method.Name == "Handle" &&
                              method.ReturnType == typeof (IObservable<Unit>) &&
                              parameters.Length == 1 &&
                              parameters[0].ParameterType == messageType
                        select method;
                    var methodInfo = methodInfoQuery.FirstOrDefault();
                    methodInfo?.Invoke(handler, new[] {e.Message});
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