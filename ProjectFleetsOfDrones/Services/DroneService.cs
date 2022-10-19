using ProjectFleetsOfDrones.DAL.Interface;
using ProjectFleetsOfDrones.Interfaces;
using ProjectFleetsOfDrones.Models;
using ProjectFleetsOfDrones.Models.Post;
using System.Text;

namespace ProjectFleetsOfDrones.Services
{
    public class DroneService : IDroneService
    {
        private readonly IDalDrone _dalDrone;

        public DroneService(IDalDrone dalDrone)
        {
            _dalDrone = dalDrone;
        }

        public Drone AddDrone(PostDroneModel drone)
        {
            var droneWithId = AddId(drone);

            //var drones = _dalDrone.ReadDrones().ToList();
            //drones.Add(droneWithId);
            //_dalDrone.WriteDrones(drones);

            _dalDrone.WriteSingleDrone(droneWithId);
            
            return droneWithId;
        }

        private int GetId()
        {
            return _dalDrone.ReadDrones().Max(drone => drone.DroneId)+1;
        }

        private Drone AddId(PostDroneModel drone)
        {
            Drone droneWithId = new();
            
            droneWithId.DroneId = GetId();
            droneWithId.FlightTime = drone.FlightTime;
            droneWithId.Propulsion = (Drone.PropulsionType)drone.Propulsion;
            droneWithId.Pilot = (Drone.PilotType)drone.Pilot;

            return droneWithId;
        }
    }
}
