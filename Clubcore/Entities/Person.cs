using Microsoft.EntityFrameworkCore;

namespace Clubcore.Entities
{
    public class Person : IEntity
    {
        public Guid PersonId { get; set; }
        public required PersonName Name { get; set; }
        public ICollection<Role> Roles { get; set; } = [];
        public ICollection<Feedback> Feedbacks { get; set; } = [];
        public ICollection<Group> Groups { get; set; } = [];
    }

    [Owned]
    public class PersonName
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNr { get; set; }
    }
}
