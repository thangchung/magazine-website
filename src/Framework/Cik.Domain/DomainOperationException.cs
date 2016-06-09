using System;

namespace Cik.Domain
{
    public class DomainOperationException : Exception
    {
        private AggregateId AggregateId { get; }

        public DomainOperationException(AggregateId aggregateId, string message)
        {
            AggregateId = aggregateId;
        }    
    }
}