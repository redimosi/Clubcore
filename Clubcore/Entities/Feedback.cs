namespace Clubcore.Entities
{
    public class Feedback : IEntity
    {
        public int FeedbackId { get; set; }
        public string Content { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}
