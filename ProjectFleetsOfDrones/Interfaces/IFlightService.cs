﻿using ProjectFleetsOfDrones.Models;
using ProjectFleetsOfDrones.Models.Post;

namespace ProjectFleetsOfDrones.Interfaces
{
    public interface IFlightService
    {
        public Flight AddFlight(PostFlightModel flight);
        public FlightWithDrone GetDetailsFlight(int id);
        public List<Flight> GetFlights();
        public Flight InsertDroneToFlight(int idFlight, int idDrone);
    }
}
