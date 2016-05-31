using System;

namespace Cik.Data.Entity.Abstraction
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}