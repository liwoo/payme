namespace Core.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public decimal Amount { get; set; }
        public string ReferenceNumber { get; set; }
    }
}