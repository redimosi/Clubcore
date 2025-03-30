using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Clubcore.Entities
{
    public class Club : IEntity
    {
        public Guid ClubId { get; set; }
        public string Name { get; set; }
        public List<Group> Groups =[];
        public void AddGroup(Group group)
        {
            Groups.Add(group);
            group.AddClub(this);
        }
        public void RemoveGroup(Group group)
        {
            Groups.Remove(group);
            group.RemoveClub(this);
        }

    }
}
