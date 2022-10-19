using ProjectFleetsOfDrones.Models;

namespace ProjectFleetsOfDrones.DAL.Interface
{
    public interface IDal
    {
        public IEnumerable<Flight> ReadFlights();
        public IEnumerable<Drone> ReadDrones();
        public void WriteFlights(IEnumerable<Flight> flights);
    }
}