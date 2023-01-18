using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Models
{
    public class Airport_
    {

        [MaxLength(6)]
        [Required]
        public int Airport_Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Workers_quont { get; set; }
        public int Passengers_quont { get; set; }
        public int Planes_quont { get; set; }
        public int Gates_quont { get; set; }
        public List<Worker> Worker { get; set; } = new();
        public List<Route> Route1 { get; set; } = new();
        public List<Route> Route2 { get; set; } = new();


    }
}
