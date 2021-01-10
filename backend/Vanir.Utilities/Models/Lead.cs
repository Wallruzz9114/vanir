using System;
using Vanir.Utilities.Abstractions;
using Vanir.Utilities.Events;

namespace Vanir.Utilities.Models
{
    public class Lead : AggregateRoot
    {
        public Lead() => Apply(new LeadCreatedEvent());

        protected override void When(dynamic @event) => When(@event);

        protected void When(LeadCreatedEvent leadCreatedEvent) => LeadId = leadCreatedEvent.LeadId;

        protected void When(LeadRemovedEvent leadRemovedEvent) => Deleted = leadRemovedEvent.Deleted;

        protected override void EnsureValidState() { }

        public void Remove() { Apply(new LeadRemovedEvent()); }

        public Guid LeadId { get; private set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? Deleted { get; set; }
    }
}