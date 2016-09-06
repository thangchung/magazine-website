using System;

namespace Cik.CoreLibs.Domain
{
    public class DomainOperationException : Exception
    {
        public DomainOperationException(AggregateId aggregateId, string message) 
            : base(message)
        {
            AggregateId = aggregateId;
        }

        private AggregateId AggregateId { get; }
    }
}