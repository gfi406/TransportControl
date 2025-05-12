namespace TransportControl.Model.Entity
{
    public class TrackList : BaseEntity
    {

        public double RemainingFuelStart { get; set; }
        public double? RemainingFuelEnd { get; set; }

        // время открытия и закрытия листа
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int? OdometrStart { get; set; }
        public int? OdometrEnd { get; set; }

        //Время действия пут листа
        public DateTime ValidityPeriodStart { get; set; }
        public DateTime ValidityPeriodEnd { get; set; }

        // точки в маршруте
        public ICollection<TrackPoint>? TrackPoints { get; set; } = new List<TrackPoint>();

        public Guid? CarId { get; set; }
        public Car? Car { get; set; }

        public Guid? DriverId { get; set; }
        public Driver? Driver { get; set; }

    }
}
