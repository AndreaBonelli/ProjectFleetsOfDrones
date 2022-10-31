namespace ProjectFleetsOfDrones.Models
{
    public class DroneWithoutFlights
    {
        public int DroneId { get; set; }
        public double FlightTime { get; set; }
        public string Propulsion { get; set; }
        public string Pilot { get; set; }
    }
}
