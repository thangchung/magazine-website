namespace Cik.CoreLibs.Domain
{
    /// <summary>
    ///     This is a supertype for register all command validators
    /// </summary>
    public interface ICommandValidator<T>
        where T : Command
    {
    }
}