using System;
using Microsoft.EntityFrameworkCore;

namespace Vanir.Utilities.Entities
{
    [Index("StreamId", "Aggregate")]
    public class StoredEvent
    {
        public Guid StoredEventId { get; set; }
        public Guid StreamId { get; set; }
        public string Type { get; set; }
        public string Aggregate { get; set; }
        public string AggregateType { get; set; }
        public int Sequence { get; set; }
        public string Data { get; set; }
        public string DataType { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Version { get; set; }
    }
}