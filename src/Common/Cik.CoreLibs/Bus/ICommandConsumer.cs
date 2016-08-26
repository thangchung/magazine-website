using System.Collections.Generic;

namespace Cik.CoreLibs.Bus
{
    public interface ICommandConsumer : IMessageConsumer
    {
        IEnumerable<ICommandHandler> CommandHandlers { get; }
    }
}