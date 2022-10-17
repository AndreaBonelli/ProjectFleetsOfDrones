using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectFleetsOfDrones.Helpers;
using ProjectFleetsOfDrones.Models;
using System.Collections.Generic;
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
            string flights = Helper.Read(Helper.FlightsPath);
            List<Flight> listFlights = Helper.Deserialize<Flight>(flights);

            string drones = Helper.Read(Helper.DronesPath);
            List<Drone> listDrones = Helper.Deserialize<Drone>(drones);

            foreach (var flight in listFlights)
            {
                foreach (var drone in listDrones)
                {
                    if (flight.DroneId == drone.DroneId)
                    {
                        FlightWithDrone fwd = new();
                        fwd.FlightId = flight.FlightId;
                        fwd.StartDate = flight.StartDate;
                        fwd.EndDate = flight.EndDate;
                        fwd.Drone = drone;
                        return Ok(fwd);
                    }
                }
            }
                
            return BadRequest();
        }

        [HttpGet]
        public IActionResult Get()
        {
            string text = Helper.Read(Helper.FlightsPath);
            List<Flight> list = Helper.Deserialize<Flight>(text);

            return Ok(list);
            //return BadRequest();
        }

        [HttpPut("{idFlight}/drone/{idDrone}")]
        public IActionResult InsertDrone(int idFlight, int idDrone)
        {
            string flights = Helper.Read(Helper.FlightsPath);
            List<Flight> listFlights = Helper.Deserialize<Flight>(flights);

            string drones = Helper.Read(Helper.DronesPath);
            List<Drone> listDrones = Helper.Deserialize<Drone>(drones);

            foreach (var flight in listFlights)
            {
                foreach (var drone in listDrones)
                {
                    if (flight.FlightId == idFlight &&
                        drone.DroneId == idDrone)
                    {
                        flight.DroneId = drone.DroneId;
                        Helper.Write(Helper.FlightsPath, listFlights);
                        return Ok(flight);
                    }
                }
            }
            return BadRequest();
            
        }

        

    }
}
