namespace Cik.Services.Magazine.MagazineService.CommandHandlers
{
    public interface IHandleCommand<in T>
    {
        void Handle(T args);
    }
}