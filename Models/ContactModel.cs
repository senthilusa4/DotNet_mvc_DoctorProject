using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DocterSpot.Models
{
    public class ContactModel
    {
        [Display(Name = "Id")]
        public int ContactId { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Mobile number")]
        public string MobileNumber { get; set; }

        [Display(Name = "Remarks")]
        public string Remarks { get; set; } 
    }
}