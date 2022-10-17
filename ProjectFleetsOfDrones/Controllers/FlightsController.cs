using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectFleetsOfDrones.Models;

namespace ProjectFleetsOfDrones.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        [HttpPost]
        public IActionResult Add([FromBody] Flight flight)
        {
            Write(flight.ToString() + "\n");
            return Ok(flight);
        }

        public static void Write(string s)
        {
            System.IO.File.AppendAllText("Flights.txt", s);
        }
    }
}
