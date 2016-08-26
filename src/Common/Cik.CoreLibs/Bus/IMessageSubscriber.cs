using System;

namespace Cik.CoreLibs.Bus
{
    public interface IMessageSubscriber : IDisposable
    {
        void Subscribe();
        event EventHandler<MessageReceivedEventArgs> MessageReceived;
    }
}