using System;

namespace Cik.Domain
{
  public interface IHandlerRegistrar
  {
    void RegisterHandler<T>(Action<T> handler) where T : IMessage;
  }
}