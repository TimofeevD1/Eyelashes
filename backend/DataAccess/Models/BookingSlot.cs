namespace DataAccess.Models
{
    public enum BookingSlotStatus
    {
        Free,   
        Booked 
    }

    public class BookingSlot
    {
        public int Id { get; set; }
        public TimeSpan SlotTime { get; set; } 
        public BookingSlotStatus Status { get; set; } 
        public int? OrderId { get; set; }
        public int BookingCalendarId { get; set; }
        public BookingCalendar BookingCalendar { get; set; } 
    }
}
