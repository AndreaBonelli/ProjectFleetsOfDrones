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

builder.Services.AddScoped<IDalDrone, FileDal>();
builder.Services.AddScoped<IDalFlight, FileDal>();
builder.Services.AddScoped<IFlightService, FlightService>();
builder.Services.AddScoped<IDroneService, DroneService>();

//Singleton: viene iniettata sempre la stessa istanza a chi la richiede.
//builder.Services.AddSingleton<IList<int>>(new List<int>()
//{
//    1,2,3,4,5,6,7,8,9,10,11,12
//});

//Transient: ogni volta che viene trovata la dipendenza ad IList<int>
//Viene iniettata una nuova istanza
//builder.Services.AddTransient<IList<int>, List<int>>();


//Scoped: ogni volta che si avvia uno scope (richiesta Http), viene creata un'istanza
//di List<int> e viene iniettata a tutti quelli che dipendono da IList<int>
builder.Services.AddScoped<IList<int>, List<int>>();


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
