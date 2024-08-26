namespace Repository.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int InitialChargedValue { get; set; }
        public int IncrementedChargedValue { get; set; }
        public double Price { get; set; }

    }

}
