using ProjectFleetsOfDrones.Models.Post;
using ProjectFleetsOfDrones.Models;

namespace ProjectFleetsOfDrones.Interfaces
{
    public interface IDroneService
    {
        public Drone AddDrone(PostDroneModel drone);
    }
}
