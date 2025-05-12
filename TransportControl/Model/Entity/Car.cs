namespace TransportControl.Model.Entity
{
    public class Car : BaseEntity
    {
        public string CarName { get; set; }
        public string CarVin { get; set; }
        public string CarNumber { get; set; }
        public string CarCategory { get; set; }
        public string CarFuelType { get; set; }
        public double CarFuelUsing { get; set; }
        public int CarOdometr { get; set; }       
        public DateTime StartInsurance { get; set; }
        public DateTime EndInsurance { get; set;}
        public ICollection<TrackList> TrackLists { get; set; } = new List<TrackList>();

    }
}

