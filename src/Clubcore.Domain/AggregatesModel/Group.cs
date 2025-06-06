﻿namespace Clubcore.Domain.AggregatesModel
{
    public class Group : IEntity
    {
        public enum GroupType
        {
            Team,
            Official,
            Other
        }
        public Guid GroupId { get; set; }
        public required string Name { get; set; }
        public ICollection<Club> Clubs { get; private set; } = [];
        public ICollection<ClubGroup> ClubGroups { get; private set; } = [];
        public ICollection<PersonRole> PersonRoles { get; set; } = [];
        public GroupType Type { get; set; }
        public ICollection<Group> ParentGroups { get; private set; } = [];
        public ICollection<Group> ChildGroups { get; private set; } = [];
    }
}
