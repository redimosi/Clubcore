using Clubcore.Common.DateTime;
using Microsoft.EntityFrameworkCore;

namespace Clubcore.Entities
{
    public class ClubGroup : IEntity
    {
        public int ClubId { get; set; }
        public int GroupId { get; set; }
        public List<TimeRange> TimeRanges { get; set; } = [];
    }

    [Owned]
    public class TimeRange : Clubcore.Common.DateTime.TimeRange
    {
    }
}
