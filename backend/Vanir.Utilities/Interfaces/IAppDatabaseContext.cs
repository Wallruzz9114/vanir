using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vanir.Utilities.Abstractions;

namespace Vanir.Utilities.Interfaces
{
    public interface IAppDatabaseContext
    {
        IQueryable<T> Set<T>(List<Guid> ids = null) where T : AggregateRoot;
        TAggregateRoot Store<TAggregateRoot>(TAggregateRoot aggregateRoot) where TAggregateRoot : AggregateRoot;
        Task<TAggregateRoot> FindAsync<TAggregateRoot>(Guid guid) where TAggregateRoot : AggregateRoot;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}