using MedicalClinics.BLL;
using MedicalClinics.BLL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedicalClinics.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly Booking _booking;

        public BookingController(Booking booking)
        {
            _booking = booking;
        }

        // POST api/<BookingController>
        [HttpPost]
        public async Task<IActionResult> Post(BookingModel bookingDetails)
        {
            var result = await _booking.BookingAppointment(bookingDetails);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost]
        [Route("CancelBooking")]
        public async Task<IActionResult> Cancel(BookingModel bookingDetails)
        {
            var result = await _booking.CancelBooking(bookingDetails);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
