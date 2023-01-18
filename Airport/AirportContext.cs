using Airport.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualBasic;
using Microsoft.EntityFrameworkCore.SqlServer  ;
using Microsoft.Extensions.Configuration;
using static System.Collections.Specialized.BitVector32;
using System.Runtime.ConstrainedExecution;


namespace Airport
{
    public class AirportContext : DbContext
    {
        public DbSet<Airline> Airlines { get; set; }
        public DbSet<Airport_> Airport_s { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Plane> Planes { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Worker> Workers { get; set; }

        public AirportContext(DbContextOptions commands) : base(commands)
        {
           
        }
  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Airline>().Property(a => a.AirlineName).HasMaxLength(50);
            modelBuilder.Entity<Airline>().Property(a => a.AirlineName).IsRequired();
            modelBuilder.Entity<Passenger>().HasCheckConstraint("Age", "Age > 0 AND Age < 100");

            /*   modelBuilder.Entity<Ticket>().Property(i => i.TicketId).HasDefaultValue(000000);
               modelBuilder.Entity<Airport_>().Property(i => i.Airport_Id).HasDefaultValue(100000);
               modelBuilder.Entity<Worker>().Property(i => i.WorkerId).HasDefaultValue(222000);
               modelBuilder.Entity<Flight>().Property(i => i.FlightId).HasDefaultValue(000333);
               modelBuilder.Entity<Route>().Property(i => i.RouteId).HasDefaultValue(002233);
               modelBuilder.Entity<Plane>().Property(i => i.PlaneId).HasDefaultValue(112233);
            */
            modelBuilder.Entity<Worker>().HasKey(i => new { i.WorkerId, i.Surname });

            modelBuilder.Entity<Airport_>().HasAlternateKey(i => new { i.Name, i.Address });

            modelBuilder.Entity<Ticket>().HasOne(t => t.Passenger).WithMany(p => p.Tickets).HasForeignKey(t => t.Passenger_Doc_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Ticket>().HasOne(t => t.Flight).WithMany(f => f.Ticket).HasForeignKey(t => t.Flight_Id).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Route>().HasOne(r => r.Airport_1 ).WithMany(a => a.Route1).HasForeignKey(r => r.Airport_ID_1).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Route>().HasOne(r => r.Airport_2).WithMany(a => a.Route2).HasForeignKey(r => r.Airport_ID_2).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Route>().HasMany(r => r.Flights).WithOne(f => f.Route).HasForeignKey(f => f.Route_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Route>().HasOne(r => r.Airline).WithMany(a => a.Route).HasForeignKey(r => r.Airline_name).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Flight>().HasMany(f => f.Ticket).WithOne(t => t.Flight).HasForeignKey(t => t.Flight_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Flight>().HasOne(f => f.Plane).WithMany(p => p.Flight).HasForeignKey(f => f.Plane_Id).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Flight>().HasOne(f => f.Route).WithMany(r => r.Flights).HasForeignKey(f => f.Route_Id).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Plane>().HasOne(p => p.Airline).WithMany(a => a.Plane).HasForeignKey(p => p.Airline_Name).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Plane>().HasMany(p => p.Flight).WithOne(f => f.Plane).HasForeignKey(f => f.Plane_Id).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Airline>().HasMany(a => a.Plane).WithOne(p => p.Airline).HasForeignKey(p => p.Airline_Name).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Airline>().HasMany(a => a.Route).WithOne(r => r.Airline).HasForeignKey(r => r.Airline_name).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Worker>().HasOne(w => w.Airport).WithMany(a => a.Worker).HasForeignKey(w => w.AirportId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Airport_>().HasMany(a_ => a_.Route1).WithOne(r => r.Airport_1).HasForeignKey(r => r.Airport_ID_1).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Airport_>().HasMany(a_ => a_.Route2).WithOne(r => r.Airport_2).HasForeignKey(r => r.Airport_ID_2).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Airport_>().HasMany(a_ => a_.Worker).WithOne(w => w.Airport).HasForeignKey(r => r.AirportId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Passenger>().HasMany(p=> p.Tickets).WithOne(t => t.Passenger).HasForeignKey(t => t.Passenger_Doc_Id).OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Airport_>().HasAlternateKey(i => new {i.Name,i.Address});
            modelBuilder.Entity<Worker>().HasKey(i => new { i.WorkerId, i.Surname });

            modelBuilder.Entity<Passenger>().HasData(
                new Passenger { PassengerId = 1, Age = 19, Name = "Passenger1", Surname = "Surname1" },
                new Passenger { PassengerId = 2, Age = 20, Name = "Passenger2", Surname = "Surname2" },
                new Passenger { PassengerId = 3, Age = 21, Name = "Passenger3", Surname = "Surname3" }

                );
          


        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connect = new ConfigurationBuilder()
                              .SetBasePath(Directory.GetCurrentDirectory())
                              .AddJsonFile("appsettings.json").Build()
                              .GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connect!);
        }





    }
}

    
