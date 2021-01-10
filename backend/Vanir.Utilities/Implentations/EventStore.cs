using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Vanir.Utilities.Abstractions;
using Vanir.Utilities.Entities;
using Vanir.Utilities.Interfaces;

namespace Vanir.Utilities.Implentations
{
    public class EventStore : IEventStore
    {
        private readonly IEventStoreContext _eventStoreContext;
        private readonly List<StoredEvent> _changes = new();

        public EventStore(IEventStoreContext eventStoreContext) => _eventStoreContext = eventStoreContext;

        public void Store(AggregateRoot aggregateRoot)
        {
            var type = aggregateRoot.GetType();
            var aggregateId = (Guid)type.GetProperty($"{ type.Name }Id").GetValue(aggregateRoot, null);
            var aggregateName = aggregateRoot.GetType().Name;

            foreach (var @event in aggregateRoot.DomainEvents)
            {
                var newEvent = new StoredEvent()
                {
                    StoredEventId = Guid.NewGuid(),
                    Aggregate = aggregateName,
                    AggregateType = type.AssemblyQualifiedName,
                    Data = JsonConvert.SerializeObject(@event),
                    StreamId = aggregateId,
                    DataType = @event.GetType().AssemblyQualifiedName,
                    Type = @event.GetType().Name,
                    CreatedOn = DateTime.UtcNow,
                    Sequence = 0
                };

                _changes.Add(newEvent);
            }

            aggregateRoot.ClearChanges();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            foreach (var @event in _changes)
                _eventStoreContext.StoredEvents.Add(@event);

            var result = await _eventStoreContext.SaveChangesAsync(cancellationToken);
            _changes.Clear();

            return result;
        }
    }
}