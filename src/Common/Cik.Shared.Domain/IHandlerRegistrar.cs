using System;

namespace Cik.Shared.Domain
{
    public interface IHandlerRegistrar
    {
        void RegisterHandler<T>(Action<T> handler) where T : IMessage;
    }
}