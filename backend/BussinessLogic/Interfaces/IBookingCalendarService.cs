using BussinessLogic.Records;

namespace BussinessLogic.Interfaces
{
    public interface IBookingCalendarService
    {
        Task AddBookingSlotsAsync(DateTime date, List<BookingSlotRec> slotRecs, CancellationToken cancellationToken = default);
        Task UpdateBookingSlotAsync(int slotId, BookingSlotRec slotRec, CancellationToken cancellationToken = default);
        Task DeleteBookingSlotAsync(int slotId, CancellationToken cancellationToken = default);
        Task BookSlotAsync(int slotId, int orderId, CancellationToken cancellationToken = default);
        Task CancelBookingAsync(int slotId, CancellationToken cancellationToken = default);
        Task<List<BookingSlotRec>> GetBookingSlotsAsync(DateTime date, CancellationToken cancellationToken = default);
        Task<List<BookingCalendarRec>> GetAllBookingCalendarsAsync(CancellationToken cancellationToken = default);
    }
}