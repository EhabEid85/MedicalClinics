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
    public class PatientsController : ControllerBase
    {
        private readonly Patients _patient;

        public PatientsController(Patients patient)
        {
            _patient = patient;
        }

        // POST api/<PatientsController>
        [HttpPost]
        public async Task<IActionResult> Post(PatientModel patientDetails)
        {
            var result = await _patient.NewPatient(patientDetails);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var result = await _patient.PatientHistory(id);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost]
        [Route("CancelBooking")]
        public async Task<IActionResult> Cancel(BookingModel bookingDetails)
        {
            var result = await _patient.PatientCancelBooking(bookingDetails);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
