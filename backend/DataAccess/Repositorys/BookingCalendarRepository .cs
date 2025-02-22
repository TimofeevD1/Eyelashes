using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositorys
{
    internal class BookingCalendarRepository : IBookingCalendarRepository
    {
        private readonly AppContext _context;

        public BookingCalendarRepository(AppContext context)
        {
            _context = context;
        }

        public async Task<List<BookingSlot>> GetBookingSlotsAsync(DateTime date, CancellationToken cancellationToken = default)
        {
            var utcDate = date.Kind == DateTimeKind.Unspecified
                ? DateTime.SpecifyKind(date, DateTimeKind.Utc)
                : date.ToUniversalTime();

            var calendar = await _context.BookingCalendars
                .Include(b => b.Slots)
                .FirstOrDefaultAsync(b => b.Date.Date == utcDate.Date, cancellationToken);

            if (calendar == null)
            {
                return new List<BookingSlot>();
            }

            return calendar.Slots;
        }

        public async Task AddBookingSlotsAsync(DateTime date, List<BookingSlot> slots, CancellationToken cancellationToken = default)
        {
            var utcDate = date.Kind == DateTimeKind.Unspecified
                ? DateTime.SpecifyKind(date, DateTimeKind.Utc)
                : date.ToUniversalTime();

            var calendar = await _context.BookingCalendars
                .Include(b => b.Slots)
                .FirstOrDefaultAsync(b => b.Date.Date == utcDate.Date, cancellationToken);

            if (calendar == null)
            {
                calendar = new BookingCalendar
                {
                    Date = utcDate,
                    Slots = new List<BookingSlot>(slots)
                };
                _context.BookingCalendars.Add(calendar);
            }
            else
            {
                foreach (var slot in slots)
                {
                    if (!calendar.Slots.Any(s => s.Id == slot.Id))
                    {
                        calendar.Slots.Add(slot);
                    }
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateBookingSlotAsync(int slotId, BookingSlot updatedSlot, CancellationToken cancellationToken = default)
        {
            var existingSlot = await _context.BookingSlots
                .FirstOrDefaultAsync(s => s.Id == slotId, cancellationToken);

            if (existingSlot == null)
            {
                throw new Exception("Слот не найден.");
            }

            existingSlot.SlotTime = updatedSlot.SlotTime;
            existingSlot.Status = updatedSlot.Status;
            existingSlot.OrderId = updatedSlot.OrderId;

            _context.BookingSlots.Update(existingSlot);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteBookingSlotAsync(int slotId, CancellationToken cancellationToken = default)
        {
            var slot = await _context.BookingSlots
                .FirstOrDefaultAsync(s => s.Id == slotId, cancellationToken);

            if (slot == null)
            {
                throw new Exception("Слот не найден.");
            }

            _context.BookingSlots.Remove(slot);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task BookSlotAsync(int slotId, int orderId, CancellationToken cancellationToken = default)
        {
            var slot = await _context.BookingSlots
                .FirstOrDefaultAsync(s => s.Id == slotId && s.Status == BookingSlotStatus.Free, cancellationToken);

            if (slot == null)
            {
                throw new Exception("Слот уже занят или не существует.");
            }

            slot.Status = BookingSlotStatus.Booked;
            slot.OrderId = orderId;
            _context.BookingSlots.Update(slot);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task CancelBookingAsync(int slotId, CancellationToken cancellationToken = default)
        {
            var slot = await _context.BookingSlots
                .FirstOrDefaultAsync(s => s.Id == slotId && s.Status == BookingSlotStatus.Booked, cancellationToken);

            if (slot == null)
            {
                throw new Exception("Запись не найдена или слот уже свободен.");
            }

            slot.Status = BookingSlotStatus.Free;
            slot.OrderId = null;
            _context.BookingSlots.Update(slot);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<BookingCalendar>> GetAllBookingCalendarsAsync(CancellationToken cancellationToken = default)
        {
            return await _context.BookingCalendars
                .Include(b => b.Slots)
                .ToListAsync(cancellationToken);
        }
    }
}
