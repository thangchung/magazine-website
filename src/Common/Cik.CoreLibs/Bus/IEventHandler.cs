using System.Threading.Tasks;

namespace Cik.CoreLibs.Bus
{
    public interface IEventHandler<in T>
    {
        Task HandleAsync(T message);
    }
}