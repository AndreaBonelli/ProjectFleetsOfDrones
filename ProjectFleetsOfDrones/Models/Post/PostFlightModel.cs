namespace ProjectFleetsOfDrones.Models.Post
{
    public class PostFlightModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? DroneId { get; set; }
    }
}
