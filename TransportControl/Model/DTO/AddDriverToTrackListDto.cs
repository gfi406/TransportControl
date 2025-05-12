namespace TransportControl.Model.DTO
{
    public class AddDriverToTrackListDto
    {
        public Guid TrackListId { get; set; }
        public Guid DriverId { get; set; }
    }
}
