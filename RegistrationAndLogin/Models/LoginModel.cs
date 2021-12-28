using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationAndLogin.Models
{
    //[Table("Login",Schema ="public")]
    public class LoginModel
    {
        // public int Id { get; set; }
       public int Id { get; set; }
        [Required(ErrorMessage ="Email is required")]
       [EmailAddress(ErrorMessage ="Add valid Email")]
        public string Email { get; set; }
        [Required(ErrorMessage ="password is required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$", ErrorMessage = "valid password")]

        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name ="Remember Me")]
        public bool RememberMe { get; set; }
        public bool TwoFactorEnabled { get; set; }

        //public static implicit operator LoginModel(string v)
        //{
        //    //throw new NotImplementedException();
        //}
    }
}
