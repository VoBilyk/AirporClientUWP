using System;
using System.Collections.Generic;


namespace AirporClientUWP.Models
{
    public class Pilot
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Experience { get; set; }

        public DateTime BirthDate { get; set; }

        public List<Guid> CrewsId { get; set; }
    }
}
