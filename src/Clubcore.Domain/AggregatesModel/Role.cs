namespace Clubcore.Domain.AggregatesModel
{
    public class Role : IEntity
    {
        public Guid RoleId { get; set; }
        public string Name { get; set; }
        public ICollection<Person> Persons { get; set; } = [];
    }
}
