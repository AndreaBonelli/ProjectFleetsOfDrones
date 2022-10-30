using ProjectFleetsOfDrones.DAL.Interface;
using ProjectFleetsOfDrones.Helpers;
using ProjectFleetsOfDrones.Models;

namespace ProjectFleetsOfDrones.DAL
{
    public class FileDataAccessService : IDataAccessService
    {
        private readonly string DronesPath = "Drones.txt";
        private readonly string FlightsPath = "Flights.txt";

        public IEnumerable<Drone> ReadDrones()
        {
            return FileHelper.ReadAndDeserialize<Drone>(DronesPath);
        }

        public IEnumerable<Flight> ReadFlights()
        {
            return FileHelper.ReadAndDeserialize<Flight>(FlightsPath);
        }

        public void WriteFlights(IEnumerable<Flight> flights)
        {
            FileHelper.Write(FlightsPath, flights);
        }

        public List<Flight> ToList() =>
            ReadFlights().ToList();

        public Flight Add(Flight flightToAdd)
        {
            var flightsFromFile = ToList();
            flightsFromFile.Add(flightToAdd);
            WriteFlights(flightsFromFile);
            return FileHelper.ReadAndDeserialize<Flight>(FlightsPath)
                .Single(flight => flight.FlightId == flightToAdd.FlightId);
        }
    }
}
