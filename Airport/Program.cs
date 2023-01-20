using Airport;
using Airport.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using static Airport.Commands;

var contextBuilder = new DbContextOptionsBuilder<AirportContext>();
var builder = contextBuilder.Options;
//using (AirportContext context = new(builder))
//{
//    Add(builder);
//    Update(builder);
//    Delete(builder);

   
    
//}


//Захист 4 лаб
// Асинхронну функцію яка додає 100 об‘єктів баз данних
// і запустити 3 рази правильно на паралельне виконання 
async Task Function(int j)
{
    using (AirportContext context = new(builder))
    {
        for (int i = 0; i < j+33; i++)
        {
            var airline = new Airline { AirlineName = "Airline " + i, Plane_quont = i, Route_quont = i };
            context.Airlines.Add(airline);
        }
        await context.SaveChangesAsync();
    }

}
using AirportContext context = new(builder);
var first = Function(1);
var second = Function(34);
var third = Function(67);
await Task.WhenAll(first, second, third);