using System;

namespace AirporClientUWP.Models
{
    public class Departure
    {
        public Guid Id { get; set; }
        
        public DateTime Time { get; set; }
        
        public Guid CrewId { get; set; }
        
        public Guid AirplaneId { get; set; }
    }
}
