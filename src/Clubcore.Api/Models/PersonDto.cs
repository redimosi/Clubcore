namespace Clubcore.Domain.Models
{
    public class PersonDto
    {
        public Guid PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MobileNr { get; set; }
    }
}
