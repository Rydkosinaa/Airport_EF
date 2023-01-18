using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Models
{
    public class Flight
    {
        [MaxLength(6)]
        [Required]
        public int FlightId { get; set; }
        [MaxLength(6)]
        [Required]
        public int Plane_Id { get; set; }
        public Plane Plane { get; set; } 
        [MaxLength(6)]
        [Required]
        public int Route_Id { get; set; }
        public Route Route { get; set; }
        public DateTime First { get; set; }
        public DateTime Second { get; set; }

        public int Gate_Number { get; set; }    
        public int Pasengers_Quont { get; set; }
        public List<Ticket> Ticket { get; set; } = new(); 

    }
}
