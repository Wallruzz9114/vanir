using System;
using Vanir.Utilities.Abstractions;

namespace Vanir.Utilities.Models
{
    public class Dashboard : AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);
        protected override void EnsureValidState() { }

        public Guid DashboadId { get; set; }

        public class DashboardCard
        {
            public Guid DashbordCardId { get; set; }
            public dynamic Options { get; set; }
        }
    }
}