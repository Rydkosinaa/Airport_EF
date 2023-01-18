using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Models
{
    public class Plane
    {
        [MaxLength(6)]
        [Required]
        public int PlaneId { get; set; }
        public string Airline_Name { get; set; }
        [MaxLength(6)]
        [Required]
        public Airline? Airline { get; set; }
        [Required]
        public int Flight_Id { get; set; }
        public List<Flight> Flight { get; set; } = new();
        public int Max_Plane_Quont { get; set; }
        public int Pilote_Quont { get; set; }
        public int Flight_Attendant_Quont { get; set; } 
        public double Carrying_Capacity { get; set; }  
        public double Fuel_Consumption { get; set; }
    }
}
