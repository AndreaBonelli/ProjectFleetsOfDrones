namespace ProjectFleetsOfDrones.Models.Post
{
    public class PostDroneModel
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
        public double FlightTime { get; set; }
        public PropulsionType Propulsion { get; set; }
        public PilotType Pilot { get; set; }
    }
}
