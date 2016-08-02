using System.Threading.Tasks;

namespace Cik.CoreLibs.Domain
{
    public interface ICommandBus
    {
        Task SendAsync<T>(T command) where T : Command;
    }
}