using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Models
{
    public class Worker : Human
    {
        [MaxLength(6)]
        [Required]
        public int WorkerId { get; set; }
        [Required]
        [MaxLength(6)]
        public int AirportId { get; set; }
        public Airport_ Airport { get; set; } 
        public int Salary { get; set; }
        public string Position { get; set; }
    }
}
