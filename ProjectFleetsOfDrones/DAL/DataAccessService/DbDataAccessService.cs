using Microsoft.EntityFrameworkCore;
using ProjectFleetsOfDrones.DAL.Interface;
using ProjectFleetsOfDrones.Models;

namespace ProjectFleetsOfDrones.DAL
{
    public class DbDataAccessService : IDataAccessService
    {
        private readonly FleetsOfDronesDbContext _ctx;
        public DbDataAccessService(FleetsOfDronesDbContext ctx)
        {
            _ctx = ctx;
        }
        public IEnumerable<Flight> ReadFlights()
        {
            //Nel caso di una getAllFiltrata, io partirò da _ctx.Flights (query sul dbset completo)
            //L'esecuzione della query non verrà effettuata da EF fino a che non voglio effettuare un'azione sul ritorno

            //Query GetAll (basequery)
            //If filtro vero ==> concateno la query di prima con il nuovo filtro
            //If filtro 2 vero =>> concateno ancora
            //...
            //...
            //return baseQuery viene eseguita / ToList / foreach

            var flights = _ctx.Flights;
            return flights;
        }

       public Flight GetFlightById(int id)
        {
            var flight = _ctx.Flights.Include(flight => flight.Drone)
                                     .Single(flight => flight.FlightId == id);
            return flight;
        }

        public Flight Add(Flight flightToAdd)
        {
            var flight = _ctx.Flights.Add(flightToAdd);   
            _ctx.SaveChanges();
            return flightToAdd;
        }

        public IEnumerable<Drone> ReadDrones()
        {
            throw new NotImplementedException();
        }


        public List<Flight> ToList()
        {
            throw new NotImplementedException();
        }

        public void WriteFlights(IEnumerable<Flight> flights)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            var flight = _ctx.Flights.Single(flight => flight.FlightId == id);
            _ctx.Flights.Remove(flight);
            _ctx.SaveChanges();
        }

        public Flight Update(Flight flight)
        {
            var flightUpdated = _ctx.Flights.Update(flight);
            _ctx.SaveChanges();
            return flightUpdated.Entity;
        }
    }
}
