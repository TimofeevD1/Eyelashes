namespace EyelashesAPI.Requests
{
    public class BookingSlotRequest
    {
        public TimeSpan Time { get; set; }
        public string Status { get; set; }
        public int? OrderId { get; set; }
    }
}
