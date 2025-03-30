namespace Clubcore.Entities
{
    public class Club : IEntity
    {
        public Guid ClubId { get; set; }
        public string Name { get; set; }
        public ICollection<Group> Groups = [];
    }
}
