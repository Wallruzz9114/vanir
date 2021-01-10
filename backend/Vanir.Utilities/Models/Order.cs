using System;
using System.Collections.Generic;
using Vanir.Utilities.Abstractions;

namespace Vanir.Utilities.Models
{
    public class Order : AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);
        protected override void EnsureValidState() { }

        public Guid OrderId { get; set; }
        public decimal Total { get; set; }
        public ICollection<LineItem> LineItems { get; set; }

        public record LineItem { }
    }
}