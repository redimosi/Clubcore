using System.Collections.Generic;

namespace Clubcore.Entities
{
    public class Club : IEntity
    {
        public int ClubId { get; set; }
        public string Name { get; set; }
        public List<Group> Groups { get; set; } = [];

    }
}
