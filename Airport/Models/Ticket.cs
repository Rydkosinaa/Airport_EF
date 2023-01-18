using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Models
{
    public class Ticket
    {
        [MaxLength(6)]
        [Required]
        public int TicketId { get; set; }
        public string? Place_Number { get; set; }
        [MaxLength(6)]

        //[Required]
        public int Flight_Id { get; set; }
        public Flight Flight { get; set; } = null!;
        public int Passenger_Doc_Id { get; set; }   
        public Passenger Passenger { get; set; }
        public int Baggage_Weight { get; set; }
    }
}
