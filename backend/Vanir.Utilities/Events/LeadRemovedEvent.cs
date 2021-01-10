using System;

namespace Vanir.Utilities.Events
{
    public class LeadRemovedEvent
    {
        public DateTime Deleted { get; } = DateTime.UtcNow;
    }
}