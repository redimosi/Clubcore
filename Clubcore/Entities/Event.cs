﻿using System.Collections.Generic;

namespace Clubcore.Entities
{
    public class Event : IEntity
    {
        public Guid EventId { get; set; }
        public string Name { get; set; }
        public List<Feedback> Feedbacks { get; set; } = new List<Feedback>();
    }
}
