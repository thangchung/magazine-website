using System.Threading.Tasks;
using Cik.CoreLibs.Domain;

namespace Cik.CoreLibs.Bus
{
    public interface ICommandBus
    {
        Task SendAsync<T>(T command) where T : Command;
    }
}