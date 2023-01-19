using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Airport.Models
{
    public class Worker : Human
    {
    
        public int WorkerId { get; set; }
        [Required]
        public int AirportId { get; set; }
        public Airport_ Airport { get; set; } 
        public int Salary { get; set; }
        public string Position { get; set; }
        public virtual Airport_? Airports { get; set; }
    }
}
