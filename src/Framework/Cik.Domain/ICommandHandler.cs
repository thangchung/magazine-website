namespace Cik.Domain
{
    public interface ICommandHandler
    {
        void Send<T>(T command) where T : Command;
    }
}