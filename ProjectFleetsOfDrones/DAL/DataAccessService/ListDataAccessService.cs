using ProjectFleetsOfDrones.DAL.Interface;
using ProjectFleetsOfDrones.Models;

namespace ProjectFleetsOfDrones.DAL
{
    public class ListDataAccessService
    {
        private IEnumerable<Flight> _flights = new List<Flight>();
        private IEnumerable<Drone> _drones = new List<Drone>();

        public IEnumerable<Drone> ReadDrones()
        {
            return _drones;
        }

        public IEnumerable<Flight> ReadFlights()
        {
            return _flights;
        }

        public void WriteFlights(IEnumerable<Flight> flights)
        {
            _flights = flights;
        }

        public List<Flight> ToList()
        {
            throw new NotImplementedException();
        }

        public Flight Add(Flight flightToAdd)
        {
            throw new NotImplementedException();
        }
    }
}
