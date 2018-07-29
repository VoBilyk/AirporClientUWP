using System;

namespace AirporClientUWP.Models
{
    public class Aeroplane
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public Guid AeroplaneTypeId { get; set; }
        
        public double Lifetime { get; set; }
    }
}
