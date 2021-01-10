using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vanir.Utilities.Abstractions;
using Vanir.Utilities.Interfaces;
using Vanir.Utilities.Models;

namespace Vanir.Utilities.Implentations
{
    public class AppDatabaseContext : DbContext, IAppDatabaseContext
    {
        private readonly IEventStore _eventStore;
        private readonly IAggregateSet _aggregateSet;

        public AppDatabaseContext(DbContextOptions<AppDatabaseContext> options, IEventStore eventStore, IAggregateSet aggregateSet) : base(options)
        {
            _eventStore = eventStore;
            _aggregateSet = aggregateSet;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Dashboard> Dashboards { get; set; }
        public DbSet<Lead> Leads { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Role> Roles { get; set; }

        public async Task<TAggregateRoot> FindAsync<TAggregateRoot>(Guid guid) where TAggregateRoot : AggregateRoot =>
            await _aggregateSet.FindAsync<TAggregateRoot>(guid);

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken) =>
            await _eventStore.SaveChangesAsync(cancellationToken);

        public IQueryable<T> Set<T>(List<Guid> ids = null) where T : AggregateRoot => _aggregateSet.Set<T>(ids);

        public TAggregateRoot Store<TAggregateRoot>(TAggregateRoot aggregateRoot) where TAggregateRoot : AggregateRoot
        {
            _eventStore.Store(aggregateRoot);
            return aggregateRoot;
        }
    }
}