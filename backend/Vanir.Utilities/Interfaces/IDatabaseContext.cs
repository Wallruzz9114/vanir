using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vanir.Utilities.Abstractions;

namespace Vanir.Utilities.Interfaces
{
    public interface IDatabaseContext
    {
        IQueryable<T> Set<T>() where T : AggregateRoot;
        void Store(AggregateRoot aggregateRoot);
        Task<TAggregateRoot> FindAsync<TAggregateRoot>(Guid guid) where TAggregateRoot : AggregateRoot;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}