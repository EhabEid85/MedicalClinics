using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalClinics.BLL.Models
{
    public class BookingModel
    {
        public long Id { get; set; }
        public long AppointmentId { get; set; }
        public long PatientId { get; set; }
        public bool IsCanceled { get; set; }
        public string CanceledReason { get; set; }
    }
}
