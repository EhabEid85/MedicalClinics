using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalClinics.BLL.Models
{
    public class AppointmentModel
    {
        public long Id { get; set; }
        public DateTime Day { get; set; }
        public DateTime TimeFrom { get; set; }
        public DateTime TimeTo { get; set; }
        public bool IsCanceled { get; set; }
        public string CanceledReason { get; set; }
    }
}
