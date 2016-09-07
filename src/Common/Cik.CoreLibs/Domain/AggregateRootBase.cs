using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using Cik.CoreLibs.Bus;

namespace Cik.CoreLibs.Domain
{
    public enum AggregateStatus
    {
        Active,
        Archive
    }

    public abstract class AggregateRootBase
    {
        private readonly Queue<Event> uncommittedEvents = new Queue<Event>();

        public AggregateStatus AggregateStatus { get; set; } = AggregateStatus.Active;

        [Key]
        public Guid Id { get; set; }

        public string CreatedBy { get; set; } = Constants.CreatedUser;
        public DateTimeOffset CreatedDate { get; set; } = Constants.CreatedDate;
        public string ModifiedBy { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }

        // public byte[] Version { get; set; }

        [NotMapped]
        public IEnumerable<Event> UncommittedEvents => uncommittedEvents;

        protected void ApplyEvent<TEvent>(TEvent evnt) where TEvent : Event
        {
            var eventHandlerMethods =
                from m in GetType().GetTypeInfo().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                let parameters = m.GetParameters()
                where m.IsDefined(typeof (EventHandlerAttribute)) &&
                      m.ReturnType == typeof (void) &&
                      parameters.Length == 1 &&
                      parameters[0].ParameterType == evnt.GetType()
                select m;

            foreach (var eventHandlerMethod in eventHandlerMethods)
            {
                eventHandlerMethod.Invoke(this, new object[] {evnt});
            }

            // store into the queue for processing later
            uncommittedEvents.Enqueue(evnt);
        }

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
            if (!(obj is AggregateRootBase)) return false;
            var other = (AggregateRootBase) obj;
            return other.Id.Equals(Id);
        }
    }
}