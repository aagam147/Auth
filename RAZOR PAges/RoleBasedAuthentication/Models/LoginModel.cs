using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RoleBasedAuthentication.Models
{
    [Table("Login", Schema = "public")]
    public class LoginModel
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public IEnumerable<LoginModel> GetUsers()
        {
            
           List<LoginModel> obj =  new List<LoginModel>() { new LoginModel { Username = "admin@gmail.com", Password = "123", Role="Admin" },
                                   new LoginModel { Username = "Guest@gmail.com", Password = "456", Role="Guest" }
   
            };
            return obj;
        }
    }
}
