namespace ClubcoreApi.Models
{
    public class ClubDto
    {
        public Guid ClubId { get; set; }
        public string Name { get; set; }
        public List<GroupDto> Groups { get; set; }
    }
}

