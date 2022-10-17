using Microsoft.AspNetCore.Mvc;
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
            Write(drone.ToString()+"\n");
            return Ok(drone);
        }

        public static void Write(string s)
        {
            System.IO.File.AppendAllText("Drones.txt", s);
        }

    }
}