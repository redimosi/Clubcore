namespace Clubcore.Entities
{
    public class Group : IEntity
    {
        public enum GroupType
        {
            Team,
            Other
        }
        public Guid GroupId { get; set; }
        public string Name { get; set; }
        public ICollection<Club> Clubs { get; private set; } = [];
        public ICollection<Person> Members { get; set; } = [];
        public GroupType Type { get; set; }
        public ICollection<Group> ParentGroups { get; private set; } = [];
        public ICollection<Group> ChildGroups { get; private set; } = [];
    }
}
