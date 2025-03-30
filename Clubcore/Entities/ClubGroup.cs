using Microsoft.EntityFrameworkCore;

namespace Clubcore.Entities
{
    public class ClubGroup : IEntity
    {
        public Guid ClubId { get; set; }
        public Guid GroupId { get; set; }
        public ICollection<TimeRange> TimeRanges { get; set; } = [];
    }

    [Owned]
    public class TimeRange : Clubcore.Common.DateTime.TimeRange
    {
    }
}
