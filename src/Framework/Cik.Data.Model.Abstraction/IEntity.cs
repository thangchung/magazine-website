using System;

namespace Cik.Data.Model.Abstraction
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}