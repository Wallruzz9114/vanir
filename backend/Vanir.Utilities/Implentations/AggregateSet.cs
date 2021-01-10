using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Vanir.Utilities.Abstractions;
using Vanir.Utilities.Entities;
using Vanir.Utilities.Extensions;
using Vanir.Utilities.Interfaces;
using static Vanir.Utilities.Abstractions.AggregateRoot;

namespace Vanir.Utilities.Implentations
{
    public class AggregateSet : IAggregateSet
    {
        private readonly IEventStoreContext _eventStoreContext;

        public AggregateSet(IEventStoreContext eventStoreContext) => _eventStoreContext = eventStoreContext;

        public async Task<TAggregateRoot> FindAsync<TAggregateRoot>(Guid guid) where TAggregateRoot : AggregateRoot
        {
            var storedEvents = GetStoredEvents(typeof(TAggregateRoot).Name, new[] { guid });

            return await storedEvents.AnyAsync()
                ? storedEvents.OrderBy(x => x.CreatedOn).Aggregate(Create<TAggregateRoot>(), Reduce)
                : null;

            static TAggregateRoot Reduce(TAggregateRoot aggregateRoot, StoredEvent storedEvent)
            {
                aggregateRoot.Apply(JsonConvert.DeserializeObject(storedEvent.Data, Type.GetType(storedEvent.DataType)));
                aggregateRoot.ClearChanges();
                return aggregateRoot;
            }
        }

        public IQueryable<TAggregateRoot> Set<TAggregateRoot>(List<Guid> ids = null) where TAggregateRoot : AggregateRoot
        {
            var aggregateName = typeof(TAggregateRoot).Name;

            return (from storedEvent in GetStoredEvents(aggregateName, ids.ToArray()).AsEnumerable()
                    group storedEvent by storedEvent.StreamId into storedEventGroup
                    orderby storedEventGroup.Key
                    select storedEventGroup).Aggregate(new List<TAggregateRoot>(), Reduce).AsQueryable();

            static List<TAggregateRoot> Reduce(List<TAggregateRoot> aggregateRoots, IGrouping<Guid, StoredEvent> group)
            {
                var aggregate = Create<TAggregateRoot>();

                group.OrderBy(x => x.CreatedOn)
                    .ForEach(x => aggregate.Apply(JsonConvert.DeserializeObject(x.Data, Type.GetType(x.DataType))));

                aggregate.ClearChanges();
                aggregateRoots.Add(aggregate);

                return aggregateRoots;
            }

            throw new NotImplementedException();
        }

        private IQueryable<StoredEvent> GetStoredEvents(string aggregateName, Guid[] streamIds = null, DateTime? createdSince = null)
        {
            createdSince ??= DateTime.UtcNow;

            return from storedEvent in _eventStoreContext.StoredEvents
                   let ids = streamIds ?? _eventStoreContext.StoredEvents
                       .Where(x => x.Aggregate == aggregateName)
                       .Select(x => x.StreamId)
                       .AsEnumerable()

                   where ids.Contains(storedEvent.StreamId) && storedEvent.CreatedOn <= createdSince
                   select storedEvent;
        }
    }
}