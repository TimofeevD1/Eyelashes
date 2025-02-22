using BussinessLogic.Interfaces;
using BussinessLogic.Records;
using DataAccess.Interfaces;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BussinessLogic.Services
{
    public class BookingCalendarService : IBookingCalendarService
    {
        private readonly IBookingCalendarRepository _bookingCalendarRepository;

        public BookingCalendarService(IBookingCalendarRepository bookingCalendarRepository)
        {
            _bookingCalendarRepository = bookingCalendarRepository;
        }

        public async Task<List<BookingSlotRec>> GetBookingSlotsAsync(DateTime date, CancellationToken cancellationToken = default)
        {
            var slots = await _bookingCalendarRepository.GetBookingSlotsAsync(date, cancellationToken);

            return slots.Select(s => new BookingSlotRec
            {
                Id = s.Id,
                Time = s.SlotTime,
                Status = s.Status,
                OrderId = s.OrderId
            }).ToList();
        }

        public async Task AddBookingSlotsAsync(DateTime date, List<BookingSlotRec> slotRecs, CancellationToken cancellationToken = default)
        {
            var slots = slotRecs.Select(sr => new BookingSlot
            {
                SlotTime = sr.Time,
                Status = sr.Status,
                OrderId = sr.OrderId
            }).ToList();

            await _bookingCalendarRepository.AddBookingSlotsAsync(date, slots, cancellationToken);
        }

        public async Task UpdateBookingSlotAsync(int slotId, BookingSlotRec slotRec, CancellationToken cancellationToken = default)
        {
            var updatedSlot = new BookingSlot
            {
                Id = slotRec.Id,
                SlotTime = slotRec.Time,
                Status = slotRec.Status,
                OrderId = slotRec.OrderId
            };

            await _bookingCalendarRepository.UpdateBookingSlotAsync(slotId, updatedSlot, cancellationToken);
        }

        public async Task DeleteBookingSlotAsync(int slotId, CancellationToken cancellationToken = default)
        {
            await _bookingCalendarRepository.DeleteBookingSlotAsync(slotId, cancellationToken);
        }

        public async Task BookSlotAsync(int slotId, int orderId, CancellationToken cancellationToken = default)
        {
            await _bookingCalendarRepository.BookSlotAsync(slotId, orderId, cancellationToken);
        }

        public async Task CancelBookingAsync(int slotId, CancellationToken cancellationToken = default)
        {
            await _bookingCalendarRepository.CancelBookingAsync(slotId, cancellationToken);
        }

        public async Task<List<BookingCalendarRec>> GetAllBookingCalendarsAsync(CancellationToken cancellationToken = default)
        {
            var calendars = await _bookingCalendarRepository.GetAllBookingCalendarsAsync(cancellationToken);

            return calendars.Select(c => new BookingCalendarRec
            {
                Date = c.Date,
                Slots = c.Slots.Select(s => new BookingSlotRec
                {
                    Id = s.Id,
                    Time = s.SlotTime,
                    Status = s.Status,
                    OrderId = s.OrderId
                }).ToList()
            }).ToList();
        }
    }
}
