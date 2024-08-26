namespace Repository.Entities
{
    public class Parameter
    {
        public int Id { get; set; }
        public int? InitialValue { get; set; }
        public int? IncrementalValue { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
    }

}
