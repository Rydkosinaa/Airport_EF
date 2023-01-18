using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Airport.Models
{
    public class Airline
    {
        
        [Required]
        [Key]
        public string? AirlineName { get; set; }
        public int Plane_quont { get; set; }
        public int Route_quont { get; set; }
        public List<Plane> Plane{ get; set; } = new();  
        public List<Route> Route{ get; set; } = new();
    }

}
