namespace Clubcore.Entities
{
    public class Event : IEntity
    {
        public Guid EventId { get; set; }
        public string Name { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; } = [];
    }
}
