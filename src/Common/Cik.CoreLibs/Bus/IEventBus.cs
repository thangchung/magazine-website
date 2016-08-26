using System.Threading.Tasks;

namespace Cik.CoreLibs.Bus
{
    public interface IEventBus
    {
        Task PublishAsync<T>(T @event) where T : Event;
    }
}