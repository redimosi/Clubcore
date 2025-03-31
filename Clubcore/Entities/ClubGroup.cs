﻿namespace Clubcore.Entities
{
    public class ClubGroup : IEntity
    {
        public Guid ClubId { get; set; }
        public Guid GroupId { get; set; }
        public ICollection<TimeRange> TimeRanges { get; set; } = [];
    }
}
