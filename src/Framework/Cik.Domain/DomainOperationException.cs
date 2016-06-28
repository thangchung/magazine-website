using System;

namespace Cik.Domain
{
  public class DomainOperationException : Exception
  {
    public DomainOperationException(AggregateId aggregateId, string message)
    {
      AggregateId = aggregateId;
    }

    private AggregateId AggregateId { get; }
  }
}