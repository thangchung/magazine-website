using Cik.CoreLibs.Domain;
using MediatR;

namespace Cik.CoreLibs.Bus.Simple
{
    public interface ISimpleCommandHandler<T> : ICommandHandler, IAsyncNotificationHandler<T>
        where T : IAsyncNotification
    {
    }
}