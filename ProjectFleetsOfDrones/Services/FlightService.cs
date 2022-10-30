using ProjectFleetsOfDrones.DAL.Interface;
using ProjectFleetsOfDrones.Helpers;
using ProjectFleetsOfDrones.Interfaces;
using ProjectFleetsOfDrones.Models;
using ProjectFleetsOfDrones.Models.Post;

namespace ProjectFleetsOfDrones.Services
{
    public class FlightService : IFlightService
    {
        private readonly IDataAccessService _dataAccessService;

        public FlightService(IDataAccessService dataAccessService)
        {
            _dataAccessService = dataAccessService;
        }

        public List<Flight> GetFlights()
        {
            return _dataAccessService.ToList();
        }
        public Flight AddFlight(PostFlightModel flightToAdd)
        {
            var flightWithId = MapToFlight(flightToAdd);
            return _dataAccessService.Add(flightWithId);
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
        public Flight InsertDroneToFlight(int idFlight, int idDrone)
        {
            var flights = _dataAccessService.ReadFlights();
            var drones = _dataAccessService.ReadDrones();
            //var flights = FileHelper.ReadAndDeserialize<Flight>(FileHelper.FlightsPath);
            //var drones = FileHelper.ReadAndDeserialize<Drone>(FileHelper.DronesPath);

            //if (flights.Count() == 0)
            //    return null;
            //if (drones.Count() == 0)
            //    return null;

            var flightToUpdate = flights.FirstOrDefault(flight => flight.FlightId == idFlight);
            var droneToInsert = drones.FirstOrDefault(drone => drone.DroneId == idDrone);

            if (flightToUpdate == default || droneToInsert == default)
                return null;

            //se il Drone preso ha altri voli previsti nel timespan del volo da modificare.

            //Voglio prendere tutti i voli a cui partecipa il drone
            var flightsOfDrone = flights.Where(flight => flight.DroneId == droneToInsert.DroneId);

            //Voglio vedere se c'è almeno un volo con timespan coincidente
            var isDroneAvailable = flightsOfDrone.All(flight =>
                                        flightToUpdate.EndDate < flight.StartDate ||
                                        flightToUpdate.StartDate > flight.EndDate);
            //flight.EndDate < flightToUpdate.StartDate);
            if (isDroneAvailable)
            {
                flightToUpdate.DroneId = droneToInsert.DroneId;
                _dataAccessService.WriteFlights(flights); //Passare solo il volo modificato
                //FileHelper.Write(FileHelper.FlightsPath, flights.ToList());
                return flightToUpdate;
            }
            else
            {
                return null;
            }
        }

        private Flight MapToFlight(PostFlightModel flightToAdd)
        {
            var flightWithId = new Flight();
            flightWithId.FlightId = GetId();
            flightWithId.StartDate = flightToAdd.StartDate;
            flightWithId.EndDate = flightToAdd.EndDate;
            flightWithId.DroneId = flightToAdd.DroneId;
            return flightWithId;
        }
        private int GetId()
        {
            var readFlights = _dataAccessService.ReadFlights();
            if(readFlights.Any())
                return readFlights.Max(flight => flight.FlightId) + 1;
            return 1;
        }
    }
}
