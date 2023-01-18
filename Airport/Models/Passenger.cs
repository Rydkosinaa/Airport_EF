using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Models
{
    public class Passenger : Human
    {
        [MaxLength(6)]
        [Required]
        public int PassengerId { get; set; }
        public int Age { get; set; }
        public List <Ticket> Tickets {get; set; }

    }
}
