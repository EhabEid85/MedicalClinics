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
    public class AppointmentController : ControllerBase
    {
        private readonly Appointments _appointment;

        public AppointmentController(Appointments appointment)
        {
            _appointment = appointment;
        }

        // GET: api/<AppointmentController>
        [HttpGet]
        public IActionResult GetByDate(DateTime searchDate)
        {
            var result = _appointment.GetAppointmentByDate(searchDate);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        // POST api/<AppointmentController>
        [HttpPost]
        public async Task<IActionResult> Post(AppointmentModel appointmentDetails)
        {
            var result = await _appointment.NewAppointment(appointmentDetails);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost]
        [Route("CancelAppointment")]
        public async Task<IActionResult> Cancel(AppointmentModel appointmentDetails)
        {
            var result = await _appointment.CancelAppointment(appointmentDetails);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
