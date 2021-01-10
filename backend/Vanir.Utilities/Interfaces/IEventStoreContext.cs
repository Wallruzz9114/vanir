using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vanir.Utilities.Entities;

namespace Vanir.Utilities.Interfaces
{
    public interface IEventStoreContext
    {
        DbSet<StoredEvent> StoredEvents { get; }
        DbSet<SnapShot> SnapShots { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}