using System;

namespace Cik.Data.Entity.Abstraction
{
    public class EntityBase : IEntity
    {
        public Guid Id { get; set; }
    }
}