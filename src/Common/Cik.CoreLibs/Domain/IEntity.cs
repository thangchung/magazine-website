using System;

namespace Cik.CoreLibs.Domain
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}