using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Vanir.Utilities.Implentations;
using Vanir.Utilities.Interfaces;
using Vanir.Utilities.Wrappers;

namespace Vanir.Utilities.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddEventStore(this IServiceCollection services, EventStoreBuilderOptions eventStoreBuilderOptions)
        {
            services.AddTransient<IEventStoreContext, EventStoreContext>();
            services.AddTransient<IAggregateSet, AggregateSet>();
            services.AddTransient<IEventStore, EventStore>();
            services.AddTransient<IAppDatabaseContext, AppDatabaseContext>();

            services.AddDbContext<EventStoreContext>(options =>
                options.UseNpgsql(eventStoreBuilderOptions.ConnectionString,
                builder => builder
                    .MigrationsAssembly(eventStoreBuilderOptions.MigrationAssembly)
                    .EnableRetryOnFailure())
                    .UseLoggerFactory(EventStoreContext.ConsoleLoggerFactory)
                    .EnableSensitiveDataLogging());
        }
    }
}