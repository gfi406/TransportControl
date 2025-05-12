namespace TransportControl.Model.DTO
{
    public class CreateTrackListDto
    {        
        public double RemainingFuelStart { get; set; }       
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }       
        public DateTime ValidityPeriodStart { get; set; }
        public DateTime ValidityPeriodEnd { get; set; }
    }
}
