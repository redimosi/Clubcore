﻿namespace Clubcore.Domain.AggregatesModel
{
    public class Person : IEntity
    {
        public Guid PersonId { get; set; }
        public required PersonDetails Details { get; set; }
        public ICollection<Role> Roles { get; set; } = [];
        public ICollection<Feedback> Feedbacks { get; set; } = [];
        public ICollection<Group> Groups { get; set; } = [];
    }

    public class PersonDetails
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? MobileNr { get; set; }
    }
}
