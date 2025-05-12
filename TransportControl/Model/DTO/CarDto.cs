namespace TransportControl.Model.DTO
{
    public class CarDto
    {
        public Guid Id { get; set; }
        public string CarName { get; set; }
        public string CarVin { get; set; }
        public string CarNumber { get; set; }
        public string CarCategory { get; set; }
        public string CarFuelType { get; set; }
        public double CarFuelUsing { get; set; }
        public int CarOdometr { get; set; }
        public DateTime StartInsurance { get; set; }
        public DateTime EndInsurance { get; set; }
        public string PersonnelNumber { get; set; }
        //public List<TrackListDto> TrackLists { get; set; } = new();
    }
}
