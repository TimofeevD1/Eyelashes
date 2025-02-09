namespace DataAccess.Models
{
    public enum OrderStatus
    {
        Created = 1,
        Confirmed = 2
    }

    public class Order
    {
        public int Id { get; set; }
        public DateTime DesiredDate { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Service { get; set; }
        public OrderStatus Status { get; set; }
    }
}
