using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DocterSpot.Models
{
    public class UserModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date of birth")]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "Age")]
        public int Age {  get; set; }
        [Display(Name = "Gender")]
        public string Gender {  get; set; }
        [Display(Name = "Mobile number")]
        public string MobileNumber {  get; set; }
        [Display(Name = "Address")]
        public string Address { get; set; }
        public string City { get; set; }
        public string State {  get; set; }
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password {  get; set; }

    }
}