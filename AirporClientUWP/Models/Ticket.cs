using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirporClientUWP.Models
{
    public class Ticket
    {
        public Guid Id { get; set; }

        public decimal Price { get; set; }

        public Guid FlightId { get; set; }
    }
}
