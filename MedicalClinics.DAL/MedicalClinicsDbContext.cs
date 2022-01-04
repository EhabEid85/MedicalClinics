using MedicalClinics.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TripFinder.DAL;

namespace MedicalClinics.DAL
{
    public class MedicalClinicsDbContext : DbContext
    {
        public MedicalClinicsDbContext() : base()
        {

        }

        public MedicalClinicsDbContext(DbContextOptions<MedicalClinicsDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(new AppConfiguration().sqlConnectionString);

            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentPatient> AppointmentPatients { get; set; }

    }
}
