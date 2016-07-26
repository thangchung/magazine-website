using System;

namespace Cik.Shared.Domain
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}