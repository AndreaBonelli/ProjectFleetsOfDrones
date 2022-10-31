using Microsoft.EntityFrameworkCore;
using ProjectFleetsOfDrones.Models;

namespace ProjectFleetsOfDrones.DAL
{
    public class FleetsOfDronesDbContext : DbContext
    {
        //Rappresentare un set di dati relativi alla classe, sui quali posso operare
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Drone> Drones { get; set; }

        public FleetsOfDronesDbContext(DbContextOptions<FleetsOfDronesDbContext> options) 
            : base(options)
        {

        }

        //Un modo per settare configurazione del db:
        //vincoli sulle entità (Relazioni, required, check....)
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.Entity<Flight>().HasOne(f => f.DroneId);
        //}
    }
}
