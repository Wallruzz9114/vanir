using System.Collections.Generic;

namespace Vanir.Utilities.Abstractions
{
    public abstract class AggregateRoot
    {
        public List<object> _events = new();
        public IReadOnlyList<object> DomainEvents => _events.AsReadOnly();

        public void RaiseDomainEvent(object @event)
        {
            _events ??= new List<object>();
            _events.Add(@event);
        }

        public void ClearChange() => _events.Clear();

        public void Apply(object @event)
        {
            When(@event);
            EnsureValidateState();
            RaiseDomainEvent(@event);
        }

        protected abstract void When(dynamic @event);
        protected abstract void EnsureValidateState();
    }
}