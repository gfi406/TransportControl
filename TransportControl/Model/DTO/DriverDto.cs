namespace TransportControl.Model.DTO
{
    public class DriverDto
    {
        public Guid Id { get; set; }
        public string? DriverName { get; set; }
        public string? DriverCategory { get; set; }
        public string PersonnelNumber { get;  set; }
        //public List<TrackListShortDto> TrackLists { get; set; } = new();
    }
}
