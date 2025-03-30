
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
        public List<Club> Clubs { get; set; } = [];
        public List<Person> Members { get; set; } = [];
        public GroupType Type { get; set; }
        public List<Group> ParentGroups { get; set; } = [];
        public List<Group> ChildGroups { get; set; } = [];

    }
}
