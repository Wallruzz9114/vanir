using System;
using Vanir.Utilities.Abstractions;

namespace Vanir.Utilities.Models
{
    public class Quote : AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);
        protected override void EnsureValidState() { }

        public Guid QuoteId { get; private set; }
        public record LineItem { }
    }
}