using MedicalClinics.DAL;
using MedicalClinics.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TripFinder.Repository
{
    public class UnitOfWork : IDisposable
    {
        private MedicalClinicsDbContext context = new MedicalClinicsDbContext();

         private GenericRepository<MedicalClinicsDbContext, Patient> patients;
         private GenericRepository<MedicalClinicsDbContext, Appointment> appointments;
         private GenericRepository<MedicalClinicsDbContext, AppointmentPatient> appointmentPatients;

        public GenericRepository<MedicalClinicsDbContext, Patient> Patients
        {
            get
            {
                if (this.patients == null)
                {
                    this.patients = new GenericRepository<MedicalClinicsDbContext, Patient>(context);
                }
                return patients;
            }
        }
        public GenericRepository<MedicalClinicsDbContext, Appointment> Appointments
        {
            get
            {
                if (this.appointments == null)
                {
                    this.appointments = new GenericRepository<MedicalClinicsDbContext, Appointment>(context);
                }
                return appointments;
            }
        }
        public GenericRepository<MedicalClinicsDbContext, AppointmentPatient> AppointmentPatients
        {
            get
            {
                if (this.appointmentPatients == null)
                {
                    this.appointmentPatients = new GenericRepository<MedicalClinicsDbContext, AppointmentPatient>(context);
                }
                return appointmentPatients;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
