using ProjectFleetsOfDrones.DAL;
using ProjectFleetsOfDrones.DAL.Interface;
using ProjectFleetsOfDrones.Helpers;
using ProjectFleetsOfDrones.Interfaces;
using ProjectFleetsOfDrones.Models;

namespace ProjectFleetsOfDrones.Services
{
    public class FlightService : IFlightService
    {
        private readonly IDal _dal;

        //TODO: Modificare le interfacce in modo da renderle generiche e separate.
        //private readonly IDalDrone _dalDrone;
        //private readonly IDalFlight _dalFlight;

        //TODO: Refactoring degli altri metodi
        //TODO: Riscrivere il metodo Write con l'ingresso di un solo volo (modificato)


        //Lista scoped iniettata dal framework e registrata in program
        public FlightService(IDal dal, IList<int> scopedList)
        {
            _dal = dal;
            scopedList.Add(0);
        }


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
            return _dal.ReadFlights().ToList();
        }

        public Flight InsertDroneToFlight(int idFlight, int idDrone)
        {
            var flights = _dal.ReadFlights();
            var drones = _dal.ReadDrones();
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
                _dal.WriteFlights(flights); //Passare solo il volo modificato
                //FileHelper.Write(FileHelper.FlightsPath, flights.ToList());
                return flightToUpdate;
            }
            else
            {
                return null;
            }
        }
    }
}
