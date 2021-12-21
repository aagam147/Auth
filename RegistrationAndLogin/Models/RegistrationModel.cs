using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationAndLogin.Models
{
    [Table("Registration",Schema ="public")]
    public class RegistrationModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Name is Required")]
        [DataType(DataType.Text,ErrorMessage ="only aphabetic is allowed")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage ="Add valid emial id")]
        public string Email { get; set; }

        [Required(ErrorMessage = "BirthDate is Required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "Mobile is Required")]
        [DataType(DataType.PhoneNumber,ErrorMessage ="Phone Number must be numberic")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$",ErrorMessage ="valid password")]

        public string Password { get; set; }
        //[NotMapped] for 
        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Your Password is not matched")]
        public string ConfirMPassword { get; set; }

    }
}
