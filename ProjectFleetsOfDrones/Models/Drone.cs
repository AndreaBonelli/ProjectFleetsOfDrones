namespace ProjectFleetsOfDrones.Models
{
    public class Drone
    {
        public enum PropulsionType
        {
            REACTION,
            FIXEDWING,
            HELIX
        }
        public enum PilotType
        {
            PILOT,
            AI
        }
        public int DroneId { get; set; }
        public TimeSpan FlightTime { get; set; }
        public PropulsionType Propulsion { get; set; }
        public PilotType Pilot { get; set; }
    }
}
