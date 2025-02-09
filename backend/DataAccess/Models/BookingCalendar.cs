namespace DataAccess.Models
{
    public class BookingCalendar
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } 
        public List<BookingSlot> Slots { get; set; }
    }
}
