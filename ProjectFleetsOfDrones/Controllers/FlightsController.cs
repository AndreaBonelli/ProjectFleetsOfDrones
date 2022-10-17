using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectFleetsOfDrones.Helpers;
using ProjectFleetsOfDrones.Models;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace ProjectFleetsOfDrones.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        [HttpPost]
        public IActionResult Add([FromBody] Flight flight)
        {
            List<Flight> list = new();
            list.Add(flight);
            Helper.Write(Helper.FlightsPath, list);
            return Ok(flight);
        }

        [HttpGet("{id}")]
        public IActionResult GetDetails(int id)
        {
            string text = Helper.Read(Helper.FlightsPath);
            List<Flight> list = Helper.Deserialize<Flight>(text);
            
            foreach(var item in list)
            {
                if (item.FlightId == id)
                {
                    return Ok(item);
                }
            }
            return BadRequest();
        }

    }
}
