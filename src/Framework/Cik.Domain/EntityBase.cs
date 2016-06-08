using System;

namespace Cik.Domain
{
    public class EntityBase : IEntity
    {
        public Guid Id { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}