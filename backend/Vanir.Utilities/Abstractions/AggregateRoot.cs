using System.Collections.Generic;
using static System.Runtime.Serialization.FormatterServices;

namespace Vanir.Utilities.Abstractions
{
    public abstract class AggregateRoot
    {
        internal List<object> _events = new();
        public IReadOnlyCollection<object> DomainEvents => _events.AsReadOnly();

        public AggregateRoot(IEnumerable<object> events)
        {
            foreach (var @event in events) { When(@event); }
        }

        protected AggregateRoot() { }

        public void RaiseDomainEvent(object @event)
        {
            _events ??= new List<object>();
            _events.Add(@event);
        }

        public void ClearChanges() => _events?.Clear();

        public void Apply(object @event)
        {
            When(@event);
            EnsureValidState();
            RaiseDomainEvent(@event);
        }

        protected abstract void When(dynamic @event);
        protected abstract void EnsureValidState();

        public static TAggregateRoot Create<TAggregateRoot>()
            => (TAggregateRoot)GetUninitializedObject(typeof(TAggregateRoot));
    }
}