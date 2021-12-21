using Microsoft.EntityFrameworkCore;
using RoleBasedAuthentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoleBasedAuthentication.Data
{
    public class RoleBasedAuthenticationDbContext :DbContext
    {
        public RoleBasedAuthenticationDbContext(DbContextOptions<RoleBasedAuthenticationDbContext> options)
          : base(options)
        {
        }

        public DbSet<RegistrationModel> registrationModels { get; set; }
        public DbSet<LoginModel> loginModels { get; set; }
    }
}
