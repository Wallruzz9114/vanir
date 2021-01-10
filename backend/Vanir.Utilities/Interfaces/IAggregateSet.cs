using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vanir.Utilities.Abstractions;

namespace Vanir.Utilities.Interfaces
{
    public interface IAggregateSet
    {
        IQueryable<TAggregateRoot> Set<TAggregateRoot>(List<Guid> ids = null) where TAggregateRoot : AggregateRoot;
        Task<TAggregateRoot> FindAsync<TAggregateRoot>(Guid guid) where TAggregateRoot : AggregateRoot;
    }
}