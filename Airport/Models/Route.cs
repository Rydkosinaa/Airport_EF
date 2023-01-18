using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Models
{
    public class Route
    {
        public int RouteId { get; set; }
        public double Distance { get; set; }
        [MaxLength(6)]
        [Required]
        public int Airport_ID_1 { get; set; }
        public Airport_ Airport_1 { get; set; }
        [MaxLength(6)]  
        [Required]
        public int Airport_ID_2 { get; set; }
        public Airport_ Airport_2 { get; set; }
        [Required]
        public string Airline_name { get; set; }
        public Airline Airline { get; set; }
        public List<Flight> Flights { get; set; } = new();


    }
}
