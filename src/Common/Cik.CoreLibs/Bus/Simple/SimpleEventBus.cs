using System;
using System.Reactive;

namespace Cik.CoreLibs.Bus.Simple
{
    public class SimpleEventBus : IEventBus
    {
        public IObservable<Unit> Publish<T>(T @event) where T : Event
        {
            throw new NotImplementedException();
        }
    }
}