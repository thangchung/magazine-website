using System;

namespace Cik.Shared.Domain
{
    public class AggregateId
    {
        private AggregateId()
        {
        }

        public AggregateId(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }

        public static AggregateId Generate()
        {
            return new AggregateId(Guid.NewGuid());
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return ((Guid) obj).Equals(this);
        }
    }
}