using Microsoft.AspNetCore.Mvc;
using ProjectFleetsOfDrones.Helpers;
using ProjectFleetsOfDrones.Interfaces;
using ProjectFleetsOfDrones.Models;
using ProjectFleetsOfDrones.Services;

namespace ProjectFleetsOfDrones.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IFlightService _flightService = new FlightService();

        [HttpPost]
        public IActionResult Add([FromBody] Flight flight) //Entro nell'action method se il modello deserializzato corrisponde al tipo Flight.
                                                           //Se non corrisponde AspnetCore restituisce 400.
        {
            var flightAdded = _flightService.AddFlight(flight);
            //return Created($"flights/{flightAdded.FlightId}",flightAdded); Created come URI
            return CreatedAtAction(
                nameof(GetDetail),  //"GetDetail" come stringa
                new
                {
                    id = flightAdded.FlightId
                },
            //    new
            //{
            //    id = flightAdded.FlightId,
            //    name = "Marco",
            //    color = "Rosso"
            //}, 
                flightAdded);
        }

        //[HttpGet("{id}/{name}/{color}")]
        [HttpGet("{id}")]
        public IActionResult GetDetail(int id)
        {
            var resultFlight = _flightService.GetDetailsFlight(id);
            if(resultFlight == null)
                return NotFound();
            return Ok(resultFlight);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var resultFlights = _flightService.GetFlights();
            return Ok(resultFlights);
        }

        [HttpPut("{idFlight}/drone/{idDrone}")]
        public IActionResult InsertDrone(int idFlight, int idDrone)
        {
            var flight = _flightService.InsertFlight(idFlight, idDrone);
            if (flight == null)
                return BadRequest();
            return Ok(flight);
        }
    }
}
