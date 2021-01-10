using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Vanir.Utilities.Abstractions;
using Vanir.Utilities.Entities;
using Vanir.Utilities.Interfaces;

namespace Vanir.Utilities.Implentations
{
    public class EventStoreContext : DbContext, IEventStoreContext
    {
        public static readonly ILoggerFactory ConsoleLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public EventStoreContext(DbContextOptions options) : base(options) { }

        public DbSet<StoredEvent> StoredEvents { get; private set; }
        public DbSet<SnapShot> SnapShots { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SnapShot>(x =>
            {
                x.Property(o => o.Data).HasConversion(
                    data => JsonConvert.SerializeObject(data),
                    data => JsonConvert.DeserializeObject<Dictionary<string, HashSet<AggregateRoot>>>(data));
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}