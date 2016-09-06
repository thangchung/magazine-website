using System;
using System.Reactive;

namespace Cik.CoreLibs.Bus
{
    public interface ICommandBus
    {
        IObservable<Unit> Send<T>(T command) where T : Command;
    }
}