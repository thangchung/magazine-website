using MediatR;

namespace Cik.CoreLibs.Domain
{
    public interface IHandleCommand<in T> : IAsyncNotificationHandler<T>
        where T : IAsyncNotification
    {
    }
}