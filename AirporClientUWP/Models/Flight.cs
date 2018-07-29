using System;
using System.Collections.Generic;

namespace AirporClientUWP.Models
{
    public class Flight
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public string DeparturePoint { get; set; }
        
        public string Destinition { get; set; }
        
        public DateTime ArrivalTime { get; set; }
        
        public DateTime DepartureTime { get; set; }

        public List<Guid> TicketsId { get; set; }
    }
}
