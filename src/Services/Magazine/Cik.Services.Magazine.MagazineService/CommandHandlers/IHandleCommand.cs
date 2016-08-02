using MediatR;

namespace Cik.Services.Magazine.MagazineService.CommandHandlers
{
    public interface IHandleCommand<in T> : IAsyncNotificationHandler<T>
        where T : IAsyncNotification
    {
    }
}