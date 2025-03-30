using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Clubcore.Entities
{
    public class Person : IEntity
    {
        public Guid PersonId { get; set; }
        public required PersonName Name { get; set; }
        public List<Role> Roles { get; set; } = [];
        public List<Feedback> Feedbacks { get; set; } = [];
        public List<Group> Groups { get; set; } = [];
    }

    [Owned]
    public class PersonName
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNr { get; set; }
    }
}
