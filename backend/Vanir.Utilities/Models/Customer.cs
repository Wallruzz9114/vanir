using System;
using Vanir.Utilities.Abstractions;

namespace Vanir.Utilities.Models
{
    public class Customer : AggregateRoot
    {
        protected override void EnsureValidState() { }
        protected override void When(dynamic @event) => When(@event);

        public Guid CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? Deleted { get; set; }
    }
}