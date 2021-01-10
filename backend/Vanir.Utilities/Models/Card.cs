using System;
using Vanir.Utilities.Abstractions;

namespace Vanir.Utilities.Models
{
    public class Card : AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);
        protected override void EnsureValidState() { }

        public Guid CardId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}