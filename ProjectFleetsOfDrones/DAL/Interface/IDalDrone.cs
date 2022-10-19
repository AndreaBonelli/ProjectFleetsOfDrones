using ProjectFleetsOfDrones.Models;

namespace ProjectFleetsOfDrones.DAL.Interface
{
    public interface IDalDrone
    {
        public IEnumerable<Drone> ReadDrones();
        public void WriteDrones(IEnumerable<Drone> drones);
    }
}
