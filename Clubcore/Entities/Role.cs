namespace Clubcore.Entities
{
    public class Role : IEntity
    {
        public Guid RoleId { get; set; }
        public string Name { get; set; }
        public Group Group { get; set; }
        public List<Person> Persons { get; set; } = [];
    }
}
