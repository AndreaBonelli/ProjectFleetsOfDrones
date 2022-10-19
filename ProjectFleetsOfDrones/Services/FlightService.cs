using System.Dynamic;
using ProjectFleetsOfDrones.DAL;
using ProjectFleetsOfDrones.DAL.Interface;
using ProjectFleetsOfDrones.Helpers;
using ProjectFleetsOfDrones.Interfaces;
using ProjectFleetsOfDrones.Models;
using ProjectFleetsOfDrones.Models.Post;

namespace ProjectFleetsOfDrones.Services
{
    public class FlightService : IFlightService
    {
        private readonly IDalFlight _dalFlight;
        private readonly IDalDrone _dalDrone;

        //Lista scoped iniettata dal framework e registrata in program
        public FlightService(IDalFlight dal)
        {
            _dalFlight = dal;
        }

        public Flight AddFlight(PostFlightModel flightToAdd)
        {
            var flightWithId = AddId(flightToAdd);

            //var flights = _dalFlight.ReadFlights().ToList();
            //flights.Add(flightWithId);
            //_dalFlight.WriteFlights(flights);
           
            _dalFlight.WriteSingleFlight(flightWithId);

            return flightWithId;
        }


        public FlightWithDrone GetDetailsFlight(int id)
        {
            var flights = _dalFlight.ReadFlights();
            var drones = _dalDrone.ReadDrones();

            var flightToShow = flights.FirstOrDefault(flight => flight.FlightId == id);
            var droneToShow = drones.FirstOrDefault(drone => drone.DroneId == flightToShow.DroneId);

            if (flightToShow == null)
                return null;

            FlightWithDrone fwd = AddDroneDetails(flightToShow, droneToShow);

            return fwd;

        }

        public List<Flight> GetFlights()
        {
            return _dalFlight.ReadFlights().ToList();
        }

        public Flight InsertDroneToFlight(int idFlight, int idDrone)
        {
            var flights = _dalFlight.ReadFlights();
            var drones = _dalDrone.ReadDrones();
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
                _dalFlight.WriteFlights(flights); //Passare solo il volo modificato
                //FileHelper.Write(FileHelper.FlightsPath, flights.ToList());
                return flightToUpdate;
            }
            else
            {
                return null;
            }
        }

        private int GetId()
        {
            return _dalFlight.ReadFlights().Max(flight => flight.FlightId) + 1;
        }

        private Flight AddId(PostFlightModel flightToAdd)
        {
            var flightWithId = new Flight();
            flightWithId.FlightId = GetId();
            flightWithId.StartDate = flightToAdd.StartDate;
            flightWithId.EndDate = flightToAdd.EndDate;
            flightWithId.DroneId = flightToAdd.DroneId;

            return flightWithId;
        }

        private FlightWithDrone AddDroneDetails(Flight flightToShow, Drone droneToShow)
        {
            FlightWithDrone fwd = new();
            fwd.FlightId = flightToShow.FlightId;
            fwd.StartDate = flightToShow.StartDate;
            fwd.EndDate = flightToShow.EndDate;
            fwd.Drone = droneToShow;

            return fwd;
        }
    }
}
