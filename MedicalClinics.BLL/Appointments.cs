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
    public class Appointments
    {
        private readonly UnitOfWork _uow;
        private readonly WSResponse<object> _wSResponse;

        public Appointments(UnitOfWork uow, WSResponse<object> wSResponse)
        {
            _uow = uow;
            _wSResponse = wSResponse;
        }

        public WSResponse<object> GetAppointmentByDate(DateTime date)
        {
            try
            {
                var appiontmentList = _uow.Appointments.Get(a => a.Day == date);
                return _wSResponse.ReturnWS(true, appiontmentList.ToList(), null);
            }
            catch (Exception ex)
            {
                return _wSResponse.ReturnWS(false, null, ex.Message);
            }
        }

        public async Task<WSResponse<object>> NewAppointment(AppointmentModel appointmentDetails)
        {
            try
            {
                //not allaw add old day
                if (appointmentDetails.Day < DateTime.Now)
                    return _wSResponse.ReturnWS(false, null, "Can't record an old day");

                if (appointmentDetails.TimeFrom > appointmentDetails.TimeTo)
                    return _wSResponse.ReturnWS(false, null, "Start Time greater tha End Time");

                _uow.Appointments.AddAsync(new DAL.Entities.Appointment
                {
                    Day = appointmentDetails.Day,
                    TimeFrom = appointmentDetails.TimeFrom,
                    TimeTo = appointmentDetails.TimeTo,
                    IsCanceled = false
                });
                await _uow.SaveAsync();

                return _wSResponse.ReturnWS(true, "Saved successfully", null);
            }
            catch (Exception ex)
            {
                return _wSResponse.ReturnWS(false, null, ex.Message);
            }

        }

        public async Task<WSResponse<object>> CancelAppointment(AppointmentModel appointmentDetails)
        {
            try
            {
                if (appointmentDetails.Id == 0)
                    return _wSResponse.ReturnWS(false, null, "Appointment not found");

                //Check if this Appointment is exist
                var isExist = await _uow.Appointments.GetById(appointmentDetails.Id);

                if (isExist == null)
                    return _wSResponse.ReturnWS(false, null, "this appointment not found");

                isExist.IsCanceled = true;
                isExist.CanceledReason = appointmentDetails.CanceledReason;

                _uow.Appointments.Update(isExist);
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
