using ProjectFleetsOfDrones.DAL.Interface;
using ProjectFleetsOfDrones.Helpers;
using ProjectFleetsOfDrones.Models;

namespace ProjectFleetsOfDrones.DAL
{
    public class FileDal : IDalDrone, IDalFlight
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

        public void WriteDrones(IEnumerable<Drone> drones)
        {
            FileHelper.Write(DronesPath, drones);
        }

        public void WriteFlights(IEnumerable<Flight> flights)
        {
            FileHelper.Write(FlightsPath, flights);
        }
    }
}
