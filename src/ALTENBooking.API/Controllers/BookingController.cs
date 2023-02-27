using ALTENBooking.Application.DTOs;
using ALTENBooking.Application.Interfaces;
using ALTENBooking.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ALTENBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {        
        private readonly IBookingService _bookingService;
        public BookingController(IBookingService bookingService)
        {            
            _bookingService = bookingService;
        }

        [HttpGet]
        public IActionResult Bookings()
        {
            return Ok(_bookingService.GetReservation());
        }

        [HttpGet("PeriodStatus")]
        public IActionResult PeriodStatus(DateTime startDate, DateTime endDate)
        {
            bool isDateAvailable = _bookingService.IsRoomAvailable(startDate, endDate);

            ResultDTO result = new ResultDTO();
            result.Message = isDateAvailable ? StringMessages.ReservationRoomAvailable : StringMessages.ReservationRoomUnavailable;

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(BookingDTO booking)
        {
            ResultDTO result = _bookingService.DoReservation(booking);
            if (result.HasError)
                return BadRequest(result);
            else
                return Ok(result);            
        }

        [HttpPut]
        public IActionResult Update(BookingUpdateDTO booking)
        {
            ResultDTO result = _bookingService.ModifyReservation(booking);
            if (result.HasError)
                return BadRequest(result);
            else
                return Ok(result);
        }

        [HttpDelete]
        public IActionResult Delete(Guid customerId)
        {
            ResultDTO result = _bookingService.CancelReservation(customerId);
            if (result.HasError)
                return BadRequest(result);
            else
                return Ok(result);
        }
    }
}
