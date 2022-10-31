using Microsoft.EntityFrameworkCore;
using ProjectFleetsOfDrones.DAL;
using ProjectFleetsOfDrones.DAL.Interface;
using ProjectFleetsOfDrones.Interfaces;
using ProjectFleetsOfDrones.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDataAccessService, DbDataAccessService>();
builder.Services.AddScoped<IFlightService, FlightService>();

builder.Services.AddDbContext<FleetsOfDronesDbContext>(option =>
                option.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Test"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
