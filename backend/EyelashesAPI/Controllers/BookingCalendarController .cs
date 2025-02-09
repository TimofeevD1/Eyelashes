using BussinessLogic.Interfaces;
using BussinessLogic.Records;
using DataAccess.Models;
using EyelashesAPI.Requests;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("slots/{date}")]
        public async Task<IActionResult> GetSlotsAsync(string date, CancellationToken cancellationToken)
        {
            if (DateTime.TryParse(date, out var parsedDate))
            {
                var slots = await _bookingCalendarService.GetBookingSlotsAsync(parsedDate, cancellationToken);
                return Ok(slots);
            }

            return BadRequest("Invalid date format.");
        }

        [HttpPost("slots")]
        public async Task<IActionResult> AddOrUpdateSlotsAsync([FromQuery] string date, [FromBody] List<BookingSlotRequest> slotRequests, CancellationToken cancellationToken)
        {
            if (DateTime.TryParse(date, out var parsedDate)) 
            {   
                var slots = slotRequests.Select(sr => new BookingSlotRec
                {
                    Time = sr.Time,
                    Status = Enum.TryParse<BookingSlotStatus>(sr.Status, out var status) ? status : BookingSlotStatus.Free,
                    OrderId = sr.OrderId
                }).ToList();

                await _bookingCalendarService.AddOrUpdateBookingSlotsAsync(parsedDate, slots, cancellationToken);
                return Ok("Slots have been added or updated successfully."); 
            }

            return BadRequest("Invalid date format.");
        }

        [HttpPost("slots/{slotId:int}/book")]
        public async Task<IActionResult> BookSlotAsync(int slotId, [FromBody] int orderId, CancellationToken cancellationToken)
        {
            try
            {
                await _bookingCalendarService.BookSlotAsync(slotId, orderId, cancellationToken);
                return Ok($"Slot {slotId} has been booked successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("slots/{slotId:int}/cancel")]
        public async Task<IActionResult> CancelBookingAsync(int slotId, CancellationToken cancellationToken)
        {
            try
            {
                await _bookingCalendarService.CancelBookingAsync(slotId, cancellationToken);
                return Ok($"Booking for slot {slotId} has been canceled successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllBookingsAsync(CancellationToken cancellationToken)
        {
            try
            {
                var bookings = await _bookingCalendarService.GetAllBookingCalendarsAsync(cancellationToken);
                return Ok(bookings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving data: {ex.Message}");
            }
        }
    }
}
