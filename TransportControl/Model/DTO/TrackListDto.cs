namespace TransportControl.Model.DTO
{
    public class TrackListDto
    {
        public Guid Id { get; set; }
        public double RemainingFuelStart { get; set; }
        public double? RemainingFuelEnd { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int?  OdometrStart { get; set; }
        public int? OdometrEnd { get; set; }
        public DateTime ValidityPeriodStart { get; set; }
        public DateTime ValidityPeriodEnd { get; set; }
        public string PersonnelNumber { get;  set; }

        public CarDto? Car { get; set; }
        public Guid? CarId { get; set; }
        public DriverDto? Driver { get; set; }
        public Guid? DriverId { get; set; }
        public List<TrackPointDto>? TrackPoints { get; set; } = new();
    }
    public class TrackListShortDto
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
    }
}
