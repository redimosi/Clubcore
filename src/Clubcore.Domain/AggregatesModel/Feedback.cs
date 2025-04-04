namespace Clubcore.Domain.AggregatesModel
{
    public class Feedback : IEntity
    {
        public enum FeedbackType
        {
            Unknown = 0,
            Positive,
            Negative,
            Neutral
        }
        public Guid FeedbackId { get; set; }
        public Guid PersonId { get; set; }
        public Guid EventId { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public FeedbackType Type { get; set; } = FeedbackType.Unknown;

    }
}
