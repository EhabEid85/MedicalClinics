using MedicalClinics.BLL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripFinder.BLL;
using TripFinder.Repository;

namespace MedicalClinics.BLL
{
    public class Patients
    {
        private readonly UnitOfWork _uow;
        private readonly WSResponse<object> _wSResponse;

        public Patients(UnitOfWork uow, WSResponse<object> wSResponse)
        {
            _uow = uow;
            _wSResponse = wSResponse;
        }

        public async Task<WSResponse<object>> NewPatient(PatientModel patientDetails)
        {
            try
            {
                _uow.Patients.AddAsync(new DAL.Entities.Patient
                {
                    Name = patientDetails.Name
                });
                await _uow.SaveAsync();

                return _wSResponse.ReturnWS(true, "Saved successfully", null);
            }
            catch (Exception ex)
            {
                return _wSResponse.ReturnWS(false, null, ex.Message);
            }

        }

        public async Task<WSResponse<object>> PatientCancelBooking(BookingModel bookingDetails)
        {
            try
            {
                var bookingData = await _uow.AppointmentPatients.GetFirstOrDefaultAsync(a => a.Id == bookingDetails.Id);

                if(bookingData != null)
                {
                    bookingData.IsCanceled = true;
                    bookingData.CanceledReason = bookingDetails.CanceledReason;

                    _uow.AppointmentPatients.Update(bookingData);
                    await _uow.SaveAsync();

                    return _wSResponse.ReturnWS(true, "Cancel Successfully", null);
                }
                else
                    return _wSResponse.ReturnWS(false, null, "the booking not found");
            }
            catch (Exception ex)
            {
                return _wSResponse.ReturnWS(false, null, ex.Message);
            }

        }

        public async Task<WSResponse<object>> PatientHistory(long patientId)
        {
            try
            {
                var patientHistory = await _uow.AppointmentPatients.Get(a => a.PatientId == patientId).Include(a=>a.Appointment).ToListAsync();
                

                return _wSResponse.ReturnWS(true, patientHistory, null);
            }
            catch (Exception ex)
            {
                return _wSResponse.ReturnWS(false, null, ex.Message);
            }
            
        }
    }
}
