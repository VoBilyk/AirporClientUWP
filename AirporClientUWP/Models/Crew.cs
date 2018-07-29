using System;
using System.Collections.Generic;

namespace AirporClientUWP.Models
{
    public class Crew
    {
        public Guid Id { get; set; }

        public Guid PilotId { get; set; }

        public List<Guid> StewardessesId { get; set; }
    }
}
