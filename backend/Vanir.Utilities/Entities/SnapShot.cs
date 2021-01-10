using System;
using System.Collections.Generic;
using Vanir.Utilities.Abstractions;

namespace Vanir.Utilities.Entities
{
    public class SnapShot
    {
        public Guid SnapShotId { get; set; }
        public DateTime Created { get; set; }
        public IDictionary<string, HashSet<AggregateRoot>> Data { get; set; } = new Dictionary<string, HashSet<AggregateRoot>>();
    }
}