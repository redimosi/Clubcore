namespace Clubcore.Entities
{
    public class Group : IEntity
    {
        public enum GroupType
        {
            Team,
            Other
        }
        public int GroupId { get; set; }
        public string Name { get; set; }
        public int ClubId { get; set; }
        public Club Club { get; set; }
        public List<Person> Members { get; set; } = [];
        public GroupType Type { get; set; }
        public List<GroupRelationship> ParentGroups { get; set; } = new();
        public List<GroupRelationship> ChildGroups { get; set; } = new();
    }
}
