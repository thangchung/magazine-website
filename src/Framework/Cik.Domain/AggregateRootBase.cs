using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cik.Domain
{
    public enum AggregateStatus
    {
        Active,
        Archive
    }

    public abstract class AggregateRootBase
    {
        protected AggregateRootBase()
            : this(new DomainEventPublisher())
        {
        }

        protected AggregateRootBase(IDomainEventPublisher eventPublisher)
        {
            EventPublisher = eventPublisher;
        }

        public AggregateStatus AggregateStatus { get; set; } = AggregateStatus.Active;

        [Key]
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        // public byte[] Version { get; set; }

        [NotMapped]
        protected IDomainEventPublisher EventPublisher { get; private set; }

        public void MarkAsRemoved()
        {
            AggregateStatus = AggregateStatus.Archive;
        }

        public bool IsRemoved()
        {
            return AggregateStatus == AggregateStatus.Archive;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            if (obj is AggregateRootBase)
            {
                var other = (AggregateRootBase) obj;
                return other.Id != null && other.Id.Equals(Id);
            }

            return false;
        }
    }
}