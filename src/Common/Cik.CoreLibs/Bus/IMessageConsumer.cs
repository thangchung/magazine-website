using System;

namespace Cik.CoreLibs.Bus
{
    public interface IMessageConsumer : IDisposable
    {
        IMessageSubscriber Subscriber { get; }
    }
}