using Microsoft.AspNetCore.Mvc;
using ProjectFleetsOfDrones.Helpers;
using ProjectFleetsOfDrones.Models;
using System.IO;
using System.Text;

namespace ProjectFleetsOfDrones.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DronesController : ControllerBase
    {
        [HttpPost]
        public IActionResult Add([FromBody] Drone drone)
        {
            List<Drone> list = new();
            list.Add(drone);
            Helper.Write(Helper.FlightsPath, list);
            return Ok(drone);
        }


    }
}