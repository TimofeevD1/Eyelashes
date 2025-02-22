using DataAccess.Models;

namespace DataAccess.Interfaces
{
    public interface IBookingCalendarRepository
    {
        Task AddBookingSlotsAsync(DateTime date, List<BookingSlot> slots, CancellationToken cancellationToken = default);
        Task UpdateBookingSlotAsync(int slotId, BookingSlot updatedSlot, CancellationToken cancellationToken = default);
        Task DeleteBookingSlotAsync(int slotId, CancellationToken cancellationToken = default);
        Task BookSlotAsync(int slotId, int orderId, CancellationToken cancellationToken = default);
        Task CancelBookingAsync(int slotId, CancellationToken cancellationToken = default);
        Task<List<BookingSlot>> GetBookingSlotsAsync(DateTime date, CancellationToken cancellationToken = default);
        Task<List<BookingCalendar>> GetAllBookingCalendarsAsync(CancellationToken cancellationToken = default);
    }
}