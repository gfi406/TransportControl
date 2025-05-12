namespace TransportControl.Model.DTO
{
    public class CreateTrackPointDto
    {
        public int NumberPoint { get; set; }
        public string CustomerCode { get; set; }
        public string StartPointName { get; set; }
        public string EndPointName { get; set; }
        public DateTime? StartPointTime { get; set; }
        public DateTime? EndPointTime { get; set; }
        public int DistanceTraveled { get; set; }
        public Guid TrackListId { get; set; }
    }
}
