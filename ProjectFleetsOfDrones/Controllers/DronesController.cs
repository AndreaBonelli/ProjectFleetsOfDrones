using Microsoft.AspNetCore.Mvc;
using ProjectFleetsOfDrones.Helpers;
using ProjectFleetsOfDrones.Interfaces;
using ProjectFleetsOfDrones.Models;
using ProjectFleetsOfDrones.Models.Post;
using System.IO;
using System.Text;

namespace ProjectFleetsOfDrones.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DronesController : ControllerBase
    {
        private readonly IDroneService _droneService;
        
        [HttpPost]
        public IActionResult Add([FromBody] PostDroneModel drone)
        {
            Drone droneWithId = _droneService.AddDrone(drone);
            return Ok(droneWithId);
        }


    }
}