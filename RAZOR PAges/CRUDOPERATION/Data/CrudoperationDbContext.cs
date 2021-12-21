using CRUDOPERATION.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDOPERATION.Data
{
    public class CrudoperationDbContext :DbContext
    {
        public CrudoperationDbContext(DbContextOptions<CrudoperationDbContext> options)
        : base(options)
        {
        }
        public DbSet<Registration> registrations { get; set; }
    }
}
