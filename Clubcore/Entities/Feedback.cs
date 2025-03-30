namespace Clubcore.Entities
{
    public class Feedback : IEntity
    {
        public Guid FeedbackId { get; set; }
        public string Content { get; set; }
        public Guid PersonId { get; set; }
        public Person Person { get; set; }
        public Guid EventId { get; set; }
        public Event Event { get; set; }
    }
}
