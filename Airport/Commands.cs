using Airport.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport
{
    internal class Commands
    {
        static public void Add(DbContextOptions commands)
        {
            using AirportContext context = new(commands);
            //Ticket ticket1 = new Ticket() { Place_Number = "21F", Passenger_Doc_Id = 1 };
            //Ticket ticket2 = new Ticket() { Place_Number = "25A", Passenger_Doc_Id = 2 };
            //Ticket ticket3 = new Ticket() { Place_Number = "23C", Passenger_Doc_Id = 3 };
            //context.Tickets.AddRange(ticket1, ticket2, ticket3);

            Airport_ airport1 = new Airport_() { Name = "Airport1", Address = "Address1", Workers_quont = 4, Passengers_quont = 5, Planes_quont = 1, Gates_quont = 3 };
            Airport_ airport2 = new Airport_() { Name = "Airport2", Address = "Address3", Workers_quont = 5, Passengers_quont = 2, Planes_quont = 3, Gates_quont = 5 };
            Airport_ airport3 = new Airport_() { Name = "Airport3", Address = "Address3", Workers_quont = 7, Passengers_quont = 8, Planes_quont = 1, Gates_quont = 7 };
            Airport_ airport4 = new Airport_() { Name = "Airport4", Address = "Address4", Workers_quont = 2, Passengers_quont = 5, Planes_quont = 2, Gates_quont = 3 };
            context.Airport_s.AddRange(airport1, airport2, airport3, airport4);

            //Airline airline1 = new Airline() { AirlineName = "Airline1", Plane_quont = 4, Route_quont = 4 };
            //Airline airline2 = new Airline() { AirlineName = "Airline2", Plane_quont = 5, Route_quont = 8 };
            //Airline airline4 = new Airline() { AirlineName = "Airline3", Plane_quont = 10, Route_quont = 2 };
            //context.Airlines.AddRange(airline1, airline2);

            //Route route1 = new Route() { Distance = 500, Airport_ID_1 = 1, Airport_ID_2 = 2, Airline_name = "Airline1" };
            //Route route2 = new Route() { Distance = 1000, Airport_ID_1 = 2, Airport_ID_2 = 3, Airline_name = "Airline2" };
            //Route route3 = new Route() { Distance = 540, Airport_ID_1 = 1, Airport_ID_2 = 4, Airline_name = "Airline1" };
            //Route route4 = new Route() { Distance = 5745, Airport_ID_1 = 4, Airport_ID_2 = 2, Airline_name = "Airline3" };
            //context.Routes.AddRange(route1, route2, route3, route4);

            //Flight flight1 = new Flight() { Plane_Id = 1, Route_Id = 2, Gate_Number = 2, Pasengers_Quont = 40 };
            //Flight flight2 = new Flight() { Plane_Id = 2, Route_Id = 4, Gate_Number = 1, Pasengers_Quont = 70 };
            //context.Flights.AddRange(flight1, flight2);
            context.SaveChanges();

        }


        static public void Update(DbContextOptions commands)
        {
            using AirportContext context = new(commands);
            var ticket = context.Airport_s.Where(t => t.Address == "Address1").FirstOrDefault();
            if (ticket != null)
            {
                ticket.Passengers_quont = 8;
                context.SaveChanges();
            }
        }

        static public void Delete(DbContextOptions commands)
        {
            using AirportContext context = new(commands);
            var airline = context.Airlines.Where(a => a.AirlineName == "Airline1").FirstOrDefault();
            if (airline != null)
            {
                context.RemoveRange(airline);
                context.SaveChanges();
            }
        }
    }
}
