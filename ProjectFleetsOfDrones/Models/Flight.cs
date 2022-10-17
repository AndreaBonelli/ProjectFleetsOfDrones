namespace ProjectFleetsOfDrones.Models
{
    public class Flight
    {
        public int FlightId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? DroneId { get; set; }
    }
}
