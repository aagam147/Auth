using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CRUDOPERATION.Model;

namespace ScaffoldingwithRazorpage.Data
{
    public class ScaffoldingwithRazorpageContext : DbContext
    {
        public ScaffoldingwithRazorpageContext (DbContextOptions<ScaffoldingwithRazorpageContext> options)
            : base(options)
        {
        }

        public DbSet<CRUDOPERATION.Model.Registration> Registration { get; set; }
    }
}
