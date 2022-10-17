namespace ProjectFleetsOfDrones.Models
{
    public class Flight
    {
        public int FlightId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? DroneId { get; set; }

        public override string? ToString()
        {
            return $"Flight Id: {FlightId}, Start date: {StartDate}, End date: {EndDate}, Drone Id: {DroneId}";
        }
    }
}
