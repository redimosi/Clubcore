using Microsoft.EntityFrameworkCore;

namespace Clubcore.Entities
{
    [Owned]
    public class PersonName
    {
        public string PreName { get; set; }
        public string SureName { get; set; }
        public string MobileNr { get; set; }
    }
}
