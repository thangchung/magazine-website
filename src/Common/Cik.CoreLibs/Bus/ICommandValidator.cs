using Cik.CoreLibs.Domain;

namespace Cik.CoreLibs.Bus
{
    /// <summary>
    ///     This is a supertype for register all command validators
    /// </summary>
    public interface ICommandValidator<T>
        where T : Command
    {
    }
}