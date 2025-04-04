namespace Clubcore.Domain.AggregatesModel
{
    public class TimeRange
    {
        public Guid TimeRangeId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime Start { get; set; } = DateTime.UtcNow;
        public DateTime End { get; set; } = DateTime.MaxValue;
    }
}
