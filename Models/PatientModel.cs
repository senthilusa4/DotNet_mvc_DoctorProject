using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DocterSpot.Models
{
    public class PatientModel
    {
        public string DoctorId {  get; set; }

        [Display(Name = "Patient id")]
        public int PatientId { get; set; }
        [Display(Name = "Patient name")]
        public string PatientName { get; set; }
        public string Issue {  get; set; }
        [Display(Name = "Visit date")]
        [DataType(DataType.Date)]
        public DateTime VisitDate { get; set; }
        [Display(Name = "Visit time")]
        [DataType(DataType.Time)]
        public string VisitTime { get; set; }

        public int Status { get; set; }
    }
}