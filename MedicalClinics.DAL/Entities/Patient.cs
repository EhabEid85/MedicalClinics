using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MedicalClinics.DAL.Entities
{
    public class Patient
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
