namespace TransportControl.Model.Entity
{
    public class BaseEntity
    {
        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public string PersonnelNumber { get; private set; }    

        public BaseEntity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;

            Random random = new Random();
            int number = random.Next(0, 100000000);
            PersonnelNumber = number.ToString("D8");
            
        }


        public void UpdateTimestamp()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
