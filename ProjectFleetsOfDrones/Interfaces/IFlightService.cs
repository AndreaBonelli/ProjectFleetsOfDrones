using ProjectFleetsOfDrones.Models;

namespace ProjectFleetsOfDrones.Interfaces
{
    public interface IFlightService
    {
        public Flight AddFlight(Flight flight);
        public FlightWithDrone GetDetailsFlight(int id);
        public List<Flight> GetFlights();
        public Flight InsertDroneToFlight(int idFlight, int idDrone);
    }
}
