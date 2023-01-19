using Airport.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Airport
{
    internal class Commands
    {
        static public void Add(DbContextOptions commands)
        {
            using AirportContext context = new(commands);
            Ticket ticket1 = new Ticket() { Place_Number = "21F", Passenger_Doc_Id = 1 };
            Ticket ticket2 = new Ticket() { Place_Number = "25A", Passenger_Doc_Id = 1 };
            Ticket ticket3 = new Ticket() { Place_Number = "23C", Passenger_Doc_Id = 3 };
            context.Tickets.AddRange(ticket1, ticket2, ticket3);

            Airport_ airport1 = new Airport_() { Name = "Airport1", Address = "Address1", Workers_quont = 4, Passengers_quont = 5, Planes_quont = 1, Gates_quont = 3 };
            Airport_ airport2 = new Airport_() { Name = "Airport2", Address = "Address3", Workers_quont = 5, Passengers_quont = 2, Planes_quont = 3, Gates_quont = 5 };
            Airport_ airport3 = new Airport_() { Name = "Airport3", Address = "Address3", Workers_quont = 7, Passengers_quont = 8, Planes_quont = 1, Gates_quont = 7 };
            Airport_ airport4 = new Airport_() { Name = "Airport4", Address = "Address4", Workers_quont = 2, Passengers_quont = 5, Planes_quont = 2, Gates_quont = 3 };
            context.Airport_s.AddRange(airport1, airport2, airport3, airport4);

            Airline airline1 = new Airline() { AirlineName = "Airline1", Plane_quont = 4, Route_quont = 4 };
            Airline airline2 = new Airline() { AirlineName = "Airline2", Plane_quont = 5, Route_quont = 8 };
            Airline airline4 = new Airline() { AirlineName = "Airline3", Plane_quont = 10, Route_quont = 2 };
            context.Airlines.AddRange(airline1, airline2);

            Route route1 = new Route() { Distance = 500, Airport_ID_1 = 1, Airport_ID_2 = 2, Airline_name = "Airline1" };
            Route route2 = new Route() { Distance = 1000, Airport_ID_1 = 2, Airport_ID_2 = 3, Airline_name = "Airline2" };
            Route route3 = new Route() { Distance = 540, Airport_ID_1 = 1, Airport_ID_2 = 4, Airline_name = "Airline1" };
            Route route4 = new Route() { Distance = 5745, Airport_ID_1 = 4, Airport_ID_2 = 2, Airline_name = "Airline3" };
            context.Routes.AddRange(route1, route2, route3, route4);

            Flight flight1 = new Flight() { Plane_Id = 1, Route_Id = 2, Gate_Number = 2, Pasengers_Quont = 40 };
            Flight flight2 = new Flight() { Plane_Id = 2, Route_Id = 4, Gate_Number = 1, Pasengers_Quont = 70 };
            context.Flights.AddRange(flight1, flight2);

            Plane plane1 = new Plane() { PlaneId = 1, Airline_Name = "Airline1",Flight_Id= 1, Max_Plane_Quont= 100,Pilote_Quont= 2, Flight_Attendant_Quont=3,Carrying_Capacity=500,Fuel_Consumption=200 };
            Plane plane2 = new Plane() { PlaneId = 2, Airline_Name = "Airline2", Flight_Id = 2, Max_Plane_Quont = 110, Pilote_Quont = 2, Flight_Attendant_Quont = 4, Carrying_Capacity = 540, Fuel_Consumption = 220 };
            context.Planes.AddRange(plane1, plane2);

            Worker worker1 = new Worker() { WorkerId= 1, AirportId= 1, Salary= 200, Position="Position1"};
            Worker worker2 = new Worker() { WorkerId = 2, AirportId = 1, Salary = 202, Position = "Position2" };
            Worker worker3 = new Worker() { WorkerId = 3, AirportId = 2, Salary = 203, Position = "Position3" };
            Worker worker4 = new Worker() { WorkerId = 4, AirportId = 2, Salary = 234, Position = "Position4" };
            Worker worker5 = new Worker() { WorkerId = 5, AirportId = 3, Salary = 265, Position = "Position5" };
            context.Workers.AddRange(worker1, worker2, worker3, worker4, worker5);

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

        static public void Loading(DbContextOptions commands)
        {
            using (AirportContext context = new AirportContext(commands))
            {
                var passengers = context.Passengers.Include(p => p.Tickets).ToList();
                foreach (var passenger in passengers)
                    foreach (var place in passenger.Tickets)
                        Console.WriteLine("[0] [1] place [2]", passenger.Name, passenger.Surname, place.Place_Number);
            }


            //Explicit  
            using (AirportContext context = new AirportContext(commands))
            {
                var tickets = context.Tickets.ToList();
                foreach (var ticket in tickets)
                {
                    context.Passengers.Where(p => p.PassengerId == ticket.Passenger_Doc_Id).Load();
                    Console.WriteLine("[0] ", ticket.Passenger.Name);
                }
              
            }
            //Lazy
            using (AirportContext context = new AirportContext(commands))
            {
                var airports = context.Airport_s.ToList();
                foreach (Airport_ airport in airports)
                {
                    Console.Write($"{airport.Name}:");
                    foreach (Worker workers in airport.Workers)
                        Console.Write($"{workers.Name} ");
                    Console.WriteLine();
                }
            }


        }
        public static void Request(DbContextOptions commands)
        {
            //union

            using (AirportContext context = new AirportContext(commands))
            {
                //union
                var workers = context.Workers.Select(w => new
                {
                    Name = w.Name,
                    Surname = w.Surname,

                });
                var passengers = context.Passengers.Select(p => new
                {
                    Name = p.Name,
                    Surname = p.Surname,

                });

                var humans = workers.Union(passengers);
                foreach (var human in humans)
                    Console.WriteLine(human.Surname, human.Name);

                //exept
                var selector1 = context.Passengers.Where(p => p.Age > 20); // 
                var selector2 = context.Passengers.Where(p => p.Name!.Contains("Passenger4"));
                var passengers_ = selector1.Except(selector2);

                foreach (var passenger in passengers_)
                    Console.WriteLine(passenger.Name);
                //intersect
                var users = context.Passengers.Where(p => p.Age > 20).Intersect(context.Passengers.Where(p => p.Name!.Contains("Passenger5")));
                foreach (var user in users)
                    Console.WriteLine(user.Name);
                //join

                var people = context.Workers.Join(context.Airport_s, w => w.WorkerId, a => a.Airport_Id, (w, a) => new
                    {
                        Name = w.Name,
                        Surname= w.Surname,
                            Airport = a.Name,
                        Position=w.Position
                     }) ;
                foreach (var p in people)
                    Console.WriteLine($"{p.Name} {p.Surname} ({p.Airport}) - {p.Position}");

                //group by

                var groups = from w in context.Workers
                             group w by w.Airport.Name into gr
                             select new
                             {
                                 gr.Key,
                                 Count = gr.Count()
                             };
                foreach (var group in groups)
                {
                    Console.WriteLine($"{group.Key} - {group.Count}");
                }

                bool result1 = context.Workers.Any(w => w.Airport!.Name == "Airport1");
                bool result2 = context.Workers.All(w => w.Airport!.Name == "Airport2");
                int number2 = context.Workers.Count(w => w.Name!.Contains("Passenger2"));


            }
            

        }

        public static void DataNoTracking(DbContextOptions commands)
        {

            using (AirportContext context = new AirportContext(commands))
            {
                var user1 = context.Workers.FirstOrDefault();
                var user2 = context.Workers.AsNoTracking().FirstOrDefault();

                if (user1 != null && user2 != null)
                {
                    Console.WriteLine($"Before Worker1: {user1.Name}   Worker2: {user2.Name}");

                    user1.Name = "Worker228";

                    Console.WriteLine($"After Worker1: {user1.Name}   Worker2: {user2.Name}");
                }
            }
        }

        public static void StoredFunction(DbContextOptions commands)
        {
            using (AirportContext context = new AirportContext(commands))
            {
                SqlParameter parametr = new SqlParameter("@age", 20);
                var passangers = context.Passengers.FromSqlRaw("SELECT * FROM GetPassengersByAge (@age)", parametr).ToList();
                foreach (var passenger in passangers)
                    Console.WriteLine($"{passenger.Name} - {passenger.Age}");
            }

            /*

                      CREATE FUNCTION [dbo].[GetPassengersByAge]
             (
                 @age int
             )
             RETURNS @returntable TABLE
             (
                 Name nvarchar(50),
               Surname nvarchar(50),
                 Age int,
               PassengerId nvarchar(50)
             )
             AS
             BEGIN
                 INSERT @returntable
                 SELECT Name, Surname, Age, PassengerId FROM Passenger WHERE Age < @age
                 RETURN
             END
      */

        }


        public static void StoredProcedure(DbContextOptions commands)
        {
            using (AirportContext context = new AirportContext(commands))
            {
                SqlParameter parametr = new("@id", "2");
                var workers = context.Workers.FromSqlRaw("GetWorkerByAirport @id", parametr).ToList();
                foreach (var worker in workers)
                    Console.WriteLine($"{worker.Name} {worker.Surname} - {worker.AirportId}");
            }



         /*
         
             CREATE PROCEDURE [dbo].[GetWorkerByAirport]
                    @id int 
                AS
             SELECT * FROM Worker 
                   WHERE AirportId=(SELECT Id FROM Airports WHERE Id=@id)

         */

        }


    }
}
