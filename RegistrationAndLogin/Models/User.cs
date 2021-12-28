using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationAndLogin.Models
{
    public class User
    {
        public string Email { get; set; }
        public bool TwoFactorEnabled { get; set; }
    }
}
