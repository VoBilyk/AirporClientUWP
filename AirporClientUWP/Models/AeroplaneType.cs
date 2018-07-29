using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirporClientUWP.Models
{
    public class AeroplaneType
    {
        public Guid Id { get; set; }

        public string Model { get; set; }

        public int Places { get; set; }

        public int Carrying { get; set; }
    }
}
