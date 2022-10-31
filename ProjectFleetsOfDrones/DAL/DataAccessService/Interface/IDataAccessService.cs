using ProjectFleetsOfDrones.Models;

namespace ProjectFleetsOfDrones.DAL.Interface
{
    public interface IDataAccessService
    {
        public IEnumerable<Flight> ReadFlights();
        public IEnumerable<Drone> ReadDrones();
        public void WriteFlights(IEnumerable<Flight> flights);
        List<Flight> ToList();
        Flight Add(Flight flightToAdd);
        Flight GetFlightById(int id);
    }
}