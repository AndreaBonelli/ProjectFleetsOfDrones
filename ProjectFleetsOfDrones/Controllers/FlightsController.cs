using Microsoft.AspNetCore.Mvc;
using ProjectFleetsOfDrones.Interfaces;
using ProjectFleetsOfDrones.Models.Post;

namespace ProjectFleetsOfDrones.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IFlightService _flightService;

        public FlightsController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var resultFlights = _flightService.GetFlights();
            return Ok(resultFlights);
        }

        [HttpPost]
        public IActionResult Add([FromBody] PostFlightModel flight) 

        {
            var flightAdded = _flightService.AddFlight(flight);
            return Created(
                "nopath",
                flightAdded);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_flightService.GetDetailsFlight(id));
        }
    }
}
