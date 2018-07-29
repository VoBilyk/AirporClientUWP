using System;
using System.Collections.Generic;


namespace AirporClientUWP.Models
{
    public class Stewardess
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public DateTime BirthDate { get; set; }

        public List<Guid> CrewsId { get; set; }
    }
}
