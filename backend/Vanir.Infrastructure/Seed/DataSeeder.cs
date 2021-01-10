using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Vanir.Utilities.Interfaces;
using Vanir.Utilities.Models;

namespace Vanir.Infrastructure.Seed
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(IAppDatabaseContext appDatabaseContext, IConfiguration _)
        {
            var user = await appDatabaseContext.Set<User>().FirstOrDefaultAsync(x => x.Username == "jpinto3");

            user ??= new User("johndoe", "johndoe@email.com", "Qwertyuiop123!");
            appDatabaseContext.Store(user);

            appDatabaseContext.SaveChangesAsync(default).GetAwaiter().GetResult();
        }
    }
}