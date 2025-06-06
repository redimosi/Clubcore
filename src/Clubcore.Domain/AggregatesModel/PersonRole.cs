﻿namespace Clubcore.Domain.AggregatesModel
{
    public class PersonRole : IEntity
    {
        public Guid PersonId { get; set; }
        public Guid RoleId { get; set; }
        public ICollection<TimeRange> TimeRanges { get; set; } = [];
    }
}
