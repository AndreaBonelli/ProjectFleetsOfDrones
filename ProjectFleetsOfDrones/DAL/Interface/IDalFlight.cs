using ProjectFleetsOfDrones.Models;

namespace ProjectFleetsOfDrones.DAL.Interface
{
    public interface IDalFlight
    {
        public IEnumerable<Flight> ReadFlights();
        public void WriteFlights(IEnumerable<Flight> flights);
        public void WriteSingleFlight(Flight flight);
    }
}
