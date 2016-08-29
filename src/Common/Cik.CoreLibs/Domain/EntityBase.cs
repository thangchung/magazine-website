using System;

namespace Cik.CoreLibs.Domain
{
    public class EntityBase : IEntity
    {
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        public Guid Id { get; set; }
    }
}