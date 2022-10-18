using ProjectFleetsOfDrones.Helpers;
using ProjectFleetsOfDrones.Interfaces;
using ProjectFleetsOfDrones.Models;

namespace ProjectFleetsOfDrones.Services
{
    public class FlightService : IFlightService
    {
       public Flight AddFlight(Flight flightToAdd)
        {
            List<Flight> flights = new List<Flight>();
            flights.Add(flightToAdd);
            FileHelper.Write(FileHelper.FlightsPath, flights);
            return flightToAdd;
        }

        public FlightWithDrone GetDetailsFlight(int id)
        {
            string flights = FileHelper.Read(FileHelper.FlightsPath);
            List<Flight> listFlights = FileHelper.Deserialize<Flight>(flights);

            string drones = FileHelper.Read(FileHelper.DronesPath);
            List<Drone> listDrones = FileHelper.Deserialize<Drone>(drones);

            foreach (var flight in listFlights)
            {
                foreach (var drone in listDrones)
                {
                    if (flight.DroneId == drone.DroneId)
                    {
                        FlightWithDrone fwd = new();
                        fwd.FlightId = flight.FlightId;
                        fwd.StartDate = flight.StartDate;
                        fwd.EndDate = flight.EndDate;
                        fwd.Drone = drone;
                        return fwd;
                    }
                }
            }

            return null;
        }

        public List<Flight> GetFlights()
        {
            string text = FileHelper.Read(FileHelper.FlightsPath);
            List<Flight> list = FileHelper.Deserialize<Flight>(text);
            return list;
        }

        public Flight InsertFlight(int idFlight, int idDrone)
        {
            string flights = FileHelper.Read(FileHelper.FlightsPath);
            List<Flight> listFlights = FileHelper.Deserialize<Flight>(flights);

            string drones = FileHelper.Read(FileHelper.DronesPath);
            List<Drone> listDrones = FileHelper.Deserialize<Drone>(drones);

            foreach (var flight in listFlights)
            {
                foreach (var drone in listDrones)
                {
                    if (flight.FlightId == idFlight &&
                        drone.DroneId == idDrone)
                    {
                        flight.DroneId = drone.DroneId;
                        FileHelper.Write(FileHelper.FlightsPath, listFlights);
                        return flight;
                    }
                }
            }
            return null;
        }
    }
}
