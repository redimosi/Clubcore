
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
        public List<Club> Clubs { get; private set; } = [];
        public List<Person> Members { get; set; } = [];
        public GroupType Type { get; set; }
        public List<Group> ParentGroups { get; private set; } = [];
        public List<Group> ChildGroups { get; private set; } = [];
        public void AddClub(Club club)
        {
            Clubs.Add(club);
            club.Groups.Add(this);
        }
        public void RemoveClub(Club club)
        {
            Clubs.Remove(club);
            club.Groups.Remove(this);
        }
        public void AddParentGroup(Group group)
        {
            ParentGroups.Add(group);
            group.ChildGroups.Add(this);
        }
        public void RemoveParentGroup(Group group)
        {
            ParentGroups.Remove(group);
            group.ChildGroups.Remove(this);
        }
        public void AddChildGroup(Group group)
        {
            ChildGroups.Add(group);
            group.ParentGroups.Add(this);
        }
        public void RemoveChildGroup(Group group)
        {
            ChildGroups.Remove(group);
            group.ParentGroups.Remove(this);
        }

    }
}
