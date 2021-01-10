using System.Threading;
using System.Threading.Tasks;
using Vanir.Utilities.Abstractions;

namespace Vanir.Utilities.Interfaces
{
    public interface IEventStore
    {
        void Store(AggregateRoot aggregateRoot);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}