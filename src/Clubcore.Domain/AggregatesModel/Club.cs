namespace Clubcore.Domain.AggregatesModel
{
    public class Club : IEntity
    {
        public Guid ClubId { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<Group> Groups { get; set; } = [];
    }
}
