using System;

namespace Vanir.Utilities.Events
{
    public class LeadCreatedEvent
    {
        public LeadCreatedEvent()
        {
            LeadId = Guid.NewGuid();
        }

        public Guid LeadId { get; set; }
    }
}