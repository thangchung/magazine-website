using System.Collections.Generic;

namespace Cik.CoreLibs.Bus
{
    public interface IEventConsumer : IMessageConsumer
    {
        IEnumerable<IEventHandler> EventHandlers { get; }
    }
}