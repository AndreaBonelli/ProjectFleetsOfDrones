using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectFleetsOfDrones.Helpers;
using ProjectFleetsOfDrones.Models;
using System.Text.Json;

namespace ProjectFleetsOfDrones.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        [HttpPost]
        public IActionResult Add([FromBody] Flight flight)
        {
            Helper.Write(Helper.FlightsPath, Helper.Serialize(flight));
            return Ok(flight);
        }

        [HttpGet("{id}")]
        public IActionResult GetDetails(int id)
        {
            string text=Read();
            return Ok(text);
        }

        public static void Write(string s)
        {
            System.IO.File.AppendAllText("Flights.txt", s);
        }
        public static string Read()
        {
            return System.IO.File.ReadAllText("Flights.txt");
        }
    }
}
