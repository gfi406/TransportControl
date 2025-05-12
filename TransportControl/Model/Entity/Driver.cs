namespace TransportControl.Model.Entity
{
    public class Driver : BaseEntity
    {
        public string DriverName { get; set; } 
        public string DriverCategory { get; set; }
        //public ICollection<TrackList>? TrackLists { get; set; } = new List<TrackList>();
    }
}
