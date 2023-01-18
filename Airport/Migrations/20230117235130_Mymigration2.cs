using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Airport.Migrations
{
    /// <inheritdoc />
    public partial class Mymigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Airlines",
                columns: table => new
                {
                    AirlineName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Planequont = table.Column<int>(name: "Plane_quont", type: "int", nullable: false),
                    Routequont = table.Column<int>(name: "Route_quont", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airlines", x => x.AirlineName);
                });

            migrationBuilder.CreateTable(
                name: "Airport_s",
                columns: table => new
                {
                    AirportId = table.Column<int>(name: "Airport_Id", type: "int", maxLength: 6, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Workersquont = table.Column<int>(name: "Workers_quont", type: "int", nullable: false),
                    Passengersquont = table.Column<int>(name: "Passengers_quont", type: "int", nullable: false),
                    Planesquont = table.Column<int>(name: "Planes_quont", type: "int", nullable: false),
                    Gatesquont = table.Column<int>(name: "Gates_quont", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airport_s", x => x.AirportId);
                    table.UniqueConstraint("AK_Airport_s_Name_Address", x => new { x.Name, x.Address });
                });

            migrationBuilder.CreateTable(
                name: "Passengers",
                columns: table => new
                {
                    PassengerId = table.Column<int>(type: "int", maxLength: 6, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passengers", x => x.PassengerId);
                    table.CheckConstraint("Age", "Age > 0 AND Age < 100");
                });

            migrationBuilder.CreateTable(
                name: "Planes",
                columns: table => new
                {
                    PlaneId = table.Column<int>(type: "int", maxLength: 6, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AirlineName = table.Column<string>(name: "Airline_Name", type: "nvarchar(50)", nullable: false),
                    FlightId = table.Column<int>(name: "Flight_Id", type: "int", nullable: false),
                    MaxPlaneQuont = table.Column<int>(name: "Max_Plane_Quont", type: "int", nullable: false),
                    PiloteQuont = table.Column<int>(name: "Pilote_Quont", type: "int", nullable: false),
                    FlightAttendantQuont = table.Column<int>(name: "Flight_Attendant_Quont", type: "int", nullable: false),
                    CarryingCapacity = table.Column<double>(name: "Carrying_Capacity", type: "float", nullable: false),
                    FuelConsumption = table.Column<double>(name: "Fuel_Consumption", type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planes", x => x.PlaneId);
                    table.ForeignKey(
                        name: "FK_Planes_Airlines_Airline_Name",
                        column: x => x.AirlineName,
                        principalTable: "Airlines",
                        principalColumn: "AirlineName",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    RouteId = table.Column<int>(type: "int", maxLength: 6, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Distance = table.Column<double>(type: "float", nullable: false),
                    AirportID1 = table.Column<int>(name: "Airport_ID_1", type: "int", maxLength: 6, nullable: false),
                    AirportID2 = table.Column<int>(name: "Airport_ID_2", type: "int", maxLength: 6, nullable: false),
                    Airlinename = table.Column<string>(name: "Airline_name", type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.RouteId);
                    table.ForeignKey(
                        name: "FK_Routes_Airlines_Airline_name",
                        column: x => x.Airlinename,
                        principalTable: "Airlines",
                        principalColumn: "AirlineName",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Routes_Airport_s_Airport_ID_1",
                        column: x => x.AirportID1,
                        principalTable: "Airport_s",
                        principalColumn: "Airport_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Routes_Airport_s_Airport_ID_2",
                        column: x => x.AirportID2,
                        principalTable: "Airport_s",
                        principalColumn: "Airport_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Workers",
                columns: table => new
                {
                    Surname = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WorkerId = table.Column<int>(type: "int", maxLength: 6, nullable: false),
                    AirportId = table.Column<int>(type: "int", maxLength: 6, nullable: false),
                    Salary = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => new { x.WorkerId, x.Surname });
                    table.ForeignKey(
                        name: "FK_Workers_Airport_s_AirportId",
                        column: x => x.AirportId,
                        principalTable: "Airport_s",
                        principalColumn: "Airport_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    FlightId = table.Column<int>(type: "int", maxLength: 6, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlaneId = table.Column<int>(name: "Plane_Id", type: "int", maxLength: 6, nullable: false),
                    RouteId = table.Column<int>(name: "Route_Id", type: "int", maxLength: 6, nullable: false),
                    First = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Second = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GateNumber = table.Column<int>(name: "Gate_Number", type: "int", nullable: false),
                    PasengersQuont = table.Column<int>(name: "Pasengers_Quont", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.FlightId);
                    table.ForeignKey(
                        name: "FK_Flights_Planes_Plane_Id",
                        column: x => x.PlaneId,
                        principalTable: "Planes",
                        principalColumn: "PlaneId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Flights_Routes_Route_Id",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "RouteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketId = table.Column<int>(type: "int", maxLength: 6, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlaceNumber = table.Column<string>(name: "Place_Number", type: "nvarchar(max)", nullable: true),
                    FlightId = table.Column<int>(name: "Flight_Id", type: "int", maxLength: 6, nullable: false),
                    PassengerDocId = table.Column<int>(name: "Passenger_Doc_Id", type: "int", nullable: false),
                    BaggageWeight = table.Column<int>(name: "Baggage_Weight", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketId);
                    table.ForeignKey(
                        name: "FK_Tickets_Flights_Flight_Id",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "FlightId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tickets_Passengers_Passenger_Doc_Id",
                        column: x => x.PassengerDocId,
                        principalTable: "Passengers",
                        principalColumn: "PassengerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Passengers",
                columns: new[] { "PassengerId", "Age", "Name", "Surname" },
                values: new object[,]
                {
                    { 1, 19, "Passenger1", "Surname1" },
                    { 2, 20, "Passenger2", "Surname2" },
                    { 3, 21, "Passenger3", "Surname3" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flights_Plane_Id",
                table: "Flights",
                column: "Plane_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_Route_Id",
                table: "Flights",
                column: "Route_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Planes_Airline_Name",
                table: "Planes",
                column: "Airline_Name");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_Airline_name",
                table: "Routes",
                column: "Airline_name");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_Airport_ID_1",
                table: "Routes",
                column: "Airport_ID_1");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_Airport_ID_2",
                table: "Routes",
                column: "Airport_ID_2");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_Flight_Id",
                table: "Tickets",
                column: "Flight_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_Passenger_Doc_Id",
                table: "Tickets",
                column: "Passenger_Doc_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Workers_AirportId",
                table: "Workers",
                column: "AirportId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Workers");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Passengers");

            migrationBuilder.DropTable(
                name: "Planes");

            migrationBuilder.DropTable(
                name: "Routes");

            migrationBuilder.DropTable(
                name: "Airlines");

            migrationBuilder.DropTable(
                name: "Airport_s");
        }
    }
}
