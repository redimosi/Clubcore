using System.Collections.Generic;

namespace Clubcore.Entities
{
    public class Person : IEntity
    {
        public int PersonId { get; set; }
        public required PersonName Name { get; set; }
        public List<Role> Roles { get; set; } = [];
        public List<Feedback> Feedbacks { get; set; } = [];
        public List<Team> Teams { get; set; } = [];
    }
}
