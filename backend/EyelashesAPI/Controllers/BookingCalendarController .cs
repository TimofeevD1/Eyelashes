using BussinessLogic.Interfaces;
using BussinessLogic.Records;
using DataAccess.Models;
using EyelashesAPI.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EyelashesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingCalendarController : ControllerBase
    {
        private readonly IBookingCalendarService _bookingCalendarService;

        public BookingCalendarController(IBookingCalendarService bookingCalendarService)
        {
            _bookingCalendarService = bookingCalendarService;
        }

        /// <summary>
        /// Получить все слоты на определенную дату.
        /// </summary>
        [HttpGet("getSlots")]
        public async Task<IActionResult> GetSlotsAsync([FromQuery] DateTime date, CancellationToken cancellationToken)
        {
            var slots = await _bookingCalendarService.GetBookingSlotsAsync(date, cancellationToken);
            return Ok(new { success = true, data = slots });
        }

        /// <summary>
        /// Добавить новые слоты на дату.
        /// </summary>
        [HttpPost("addSlots")]
        public async Task<IActionResult> AddSlotsAsync([FromQuery] DateTime date, [FromBody] List<BookingSlotRequest> slotRequests, CancellationToken cancellationToken)
        {
            if (slotRequests == null || !slotRequests.Any())
            {
                return BadRequest(new { success = false, message = "Slot list cannot be empty." });
            }

            var slots = slotRequests.Select(sr => new BookingSlotRec
            {
                Time = sr.Time,
                Status = Enum.TryParse<BookingSlotStatus>(sr.Status, out var status) ? status : BookingSlotStatus.Free,
                OrderId = sr.OrderId
            }).ToList();

            await _bookingCalendarService.AddBookingSlotsAsync(date, slots, cancellationToken);
            return Ok(new { success = true, message = "Slots have been added successfully." });
        }

        /// <summary>
        /// Обновить существующий слот.
        /// </summary>
        [HttpPut("updateSlot/{slotId:int}")]
        public async Task<IActionResult> UpdateSlotAsync(int slotId, [FromBody] BookingSlotRequest slotRequest, CancellationToken cancellationToken)
        {
            var updatedSlot = new BookingSlotRec
            {
                Id = slotId,
                Time = slotRequest.Time,
                Status = Enum.TryParse<BookingSlotStatus>(slotRequest.Status, out var status) ? status : BookingSlotStatus.Free,
                OrderId = slotRequest.OrderId
            };

            await _bookingCalendarService.UpdateBookingSlotAsync(slotId, updatedSlot, cancellationToken);
            return Ok(new { success = true, message = $"Slot {slotId} has been updated successfully." });
        }

        /// <summary>
        /// Удалить слот по ID.
        /// </summary>
        [HttpDelete("deleteSlot/{slotId:int}")]
        public async Task<IActionResult> DeleteSlotAsync(int slotId, CancellationToken cancellationToken)
        {
            await _bookingCalendarService.DeleteBookingSlotAsync(slotId, cancellationToken);
            return Ok(new { success = true, message = $"Slot {slotId} has been deleted successfully." });
        }

        /// <summary>
        /// Забронировать слот.
        /// </summary>
        [HttpPost("slots/{slotId:int}/book")]
        public async Task<IActionResult> BookSlotAsync(int slotId, [FromBody] int orderId, CancellationToken cancellationToken)
        {
            await _bookingCalendarService.BookSlotAsync(slotId, orderId, cancellationToken);
            return Ok(new { success = true, message = $"Slot {slotId} has been booked." });
        }

        /// <summary>
        /// Отменить бронирование слота.
        /// </summary>
        [HttpPost("slots/{slotId:int}/cancel")]
        public async Task<IActionResult> CancelBookingAsync(int slotId, CancellationToken cancellationToken)
        {
            await _bookingCalendarService.CancelBookingAsync(slotId, cancellationToken);
            return Ok(new { success = true, message = $"Booking for slot {slotId} has been canceled." });
        }

        /// <summary>
        /// Получить весь календарь бронирований.
        /// </summary>
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllBookingsAsync(CancellationToken cancellationToken)
        {
            var bookings = await _bookingCalendarService.GetAllBookingCalendarsAsync(cancellationToken);
            return Ok(new { success = true, data = bookings });
        }
    }
}
