using Microsoft.EntityFrameworkCore;
using RegistrationAndLogin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationAndLogin.Data
{
    public class RegistrationandLoginDbContext :DbContext
    {
        public RegistrationandLoginDbContext(DbContextOptions<RegistrationandLoginDbContext> options)
          : base(options)
        {
        }
        public DbSet<RegistrationModel> registrations { get; set; }
        public DbSet<LoginModel> logins { get; set; }
    }
}
