namespace TransportControl.Model.DTO
{
    public class TrackListCloseDto
    {
        public Guid TrackListId { get; set; }
        public int OdometrValue { get; set; }
        public double FuelRemainder { get; set; }
        public DateTime ReturnTime { get; set; }
    }
}
