using System;
using System.Collections.Generic;
using System.Threading;

namespace Cik.Domain
{
  public class InMemoryBus : IDomainEventPublisher, ICommandHandler, IHandlerRegistrar
  {
    private readonly Dictionary<Type, List<Action<IMessage>>> _routes =
      new Dictionary<Type, List<Action<IMessage>>>();

    public void Send<T>(T command) where T : Command
    {
      List<Action<IMessage>> handlers;

      if (_routes.TryGetValue(typeof (T), out handlers))
      {
        if (handlers.Count != 1) throw new InvalidOperationException("cannot send to more than one handler");
        handlers[0](command);
      }
      else
      {
        throw new InvalidOperationException("no handler registered");
      }
    }

    public void Publish<T>(T @event) where T : Event
    {
      List<Action<IMessage>> handlers;

      if (!_routes.TryGetValue(@event.GetType(), out handlers)) return;

      foreach (var handler in handlers)
      {
        //dispatch on thread pool for added awesomeness
        var handler1 = handler;
        ThreadPool.QueueUserWorkItem(x => handler1(@event));
      }
    }

    public void RegisterHandler<T>(Action<T> handler) where T : IMessage
    {
      List<Action<IMessage>> handlers;

      if (!_routes.TryGetValue(typeof (T), out handlers))
      {
        handlers = new List<Action<IMessage>>();
        _routes.Add(typeof (T), handlers);
      }

      handlers.Add(x => handler((T) x));
    }

    public void RegisterHandler(Type message, Action<Command> handler)
    {
      List<Action<IMessage>> handlers;

      if (!_routes.TryGetValue(message, out handlers))
      {
        handlers = new List<Action<IMessage>>();
        _routes.Add(message, handlers);
      }

      handlers.Add(x => handler(x as Command));
    }
  }
}