using Airport;
using Airport.Models;
using Microsoft.EntityFrameworkCore;

using static Airport.Commands;

var contextBuilder = new DbContextOptionsBuilder<AirportContext>();
var builder = contextBuilder.Options;
using (AirportContext context = new(builder))
{
    Add(builder);
    Update(builder);
    Delete(builder);
}