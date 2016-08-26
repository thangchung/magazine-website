using System;
using System.Threading.Tasks;
using Cik.CoreLibs.Domain;

namespace Cik.CoreLibs.Bus.Simple
{
    public class SimpleEventBus : IEventBus
    {
        public Task PublishAsync<T>(T @event) where T : Event
        {
            throw new NotImplementedException();
        }
    }
}