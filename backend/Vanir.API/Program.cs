using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Vanir.Infrastructure.Seed;
using Vanir.Utilities.Helpers;
using Vanir.Utilities.Implentations;

namespace Vanir.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            DBSetup(args, host).Wait();

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static async Task DBSetup(string[] args, IHost host)
        {
            var serviceScopeFactory = (IServiceScopeFactory)host.Services.GetService(typeof(IServiceScopeFactory));

            using var scope = serviceScopeFactory.CreateScope();

            var eventStoreContext = scope.ServiceProvider.GetRequiredService<EventStoreContext>();
            var appDatabaseContext = scope.ServiceProvider.GetRequiredService<AppDatabaseContext>();
            var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

            if (args.Contains("ci")) args = new string[4] { "dropdb", "migratedb", "seeddb", "stop" };
            if (args.Contains("dropdb")) eventStoreContext.Database.EnsureDeleted();
            if (args.Contains("migratedb")) eventStoreContext.Database.Migrate();

            if (args.Contains("seeddb"))
            {
                eventStoreContext.Database.EnsureCreated();
                await DataSeeder.SeedAsync(appDatabaseContext, configuration);
            }

            if (args.Contains("secret"))
            {
                Console.WriteLine(SecretGenerator.GenerateSecret());
                Environment.Exit(0);
            }

            if (args.Contains("stop")) Environment.Exit(0);
        }
    }
}
