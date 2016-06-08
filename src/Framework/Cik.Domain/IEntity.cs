using System;

namespace Cik.Domain
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}