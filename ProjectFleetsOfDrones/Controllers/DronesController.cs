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
            Helper.Write(Helper.DronesPath, Helper.Serialize(drone));
            return Ok(drone);
        }


    }
}