using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalClinics.DAL.Entities
{
    public class AppointmentPatient
    {
        [Key]
        public long Id { get; set; }
        public long AppointmentId { get; set; }
        [ForeignKey("AppointmentId")]
        public virtual Appointment Appointment { get; set; }
        public long PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; }
        public bool IsCanceled { get; set; }
        public string CanceledReason { get; set; }
    }
}
