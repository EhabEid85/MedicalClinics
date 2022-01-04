using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalClinics.DAL.Entities
{
    public class Appointment
    {
        [Key]
        public long Id { get; set; }
        public DateTime Day { get; set; }
        public DateTime TimeFrom { get; set; }
        public DateTime TimeTo { get; set; }
        public bool IsCanceled { get; set; }
        public string CanceledReason { get; set; }
    }
}
