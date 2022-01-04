using MedicalClinics.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripFinder.BLL;
using TripFinder.Repository;

namespace MedicalClinics.BLL
{
    public class Booking
    {
        private readonly UnitOfWork _uow;
        private readonly WSResponse<object> _wSResponse;

        public Booking(UnitOfWork uow, WSResponse<object> wSResponse)
        {
            _uow = uow;
            _wSResponse = wSResponse;
        }

        public async Task<WSResponse<object>> BookingAppointment(BookingModel bookingDetails)
        {
            try
            {
                //check if this patient is exist
                var patientExist = await _uow.Patients.GetFirstOrDefaultAsync(a => a.Id == bookingDetails.PatientId);
                if(patientExist == null)
                    return _wSResponse.ReturnWS(false, null, "this patient not found");

                //check if this Appointment is exist
                var appointmentExist = await _uow.Appointments.GetFirstOrDefaultAsync(a => a.Id == bookingDetails.AppointmentId);
                if (appointmentExist == null)
                    return _wSResponse.ReturnWS(false, null, "this Appointment not found");

                //check if this appointment is booking
                var bookingExist = await _uow.AppointmentPatients.GetFirstOrDefaultAsync(a => a.AppointmentId == bookingDetails.AppointmentId);
                if (bookingExist != null)
                    return _wSResponse.ReturnWS(false, null, "this Appointment aready booking");

                _uow.AppointmentPatients.AddAsync(new DAL.Entities.AppointmentPatient
                {
                    AppointmentId = bookingDetails.AppointmentId,
                    PatientId = bookingDetails.PatientId
                });
                await _uow.SaveAsync();

                return _wSResponse.ReturnWS(true, "Saved successfully", null);
            }
            catch (Exception ex)
            {
                return _wSResponse.ReturnWS(false, null, ex.Message);
            }

        }

        public async Task<WSResponse<object>> CancelBooking(BookingModel bookingDetails)
        {
            try
            {
                //Check if this Appointment is exist
                var isExist = await _uow.AppointmentPatients.GetById(bookingDetails.Id);

                if (isExist == null)
                    return _wSResponse.ReturnWS(false, null, "this Booking not found");

                isExist.IsCanceled = true;
                isExist.CanceledReason = bookingDetails.CanceledReason;

                _uow.AppointmentPatients.Update(isExist);
                await _uow.SaveAsync();

                return _wSResponse.ReturnWS(true, "Cancel successfully", null);
            }
            catch (Exception ex)
            {
                return _wSResponse.ReturnWS(false, null, ex.Message);
            }

        }
    }
}
