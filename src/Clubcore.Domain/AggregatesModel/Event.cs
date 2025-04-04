namespace Clubcore.Domain.AggregatesModel
{
    public class Event : IEntity
    {
        public Guid EventId { get; set; }
        public string Name { get; set; } = null!;
        public TimeRange TimeRange { get; set; } = null!;
        public ICollection<Feedback> Feedbacks { get; set; } = [];
    }
}
