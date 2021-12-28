using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ScaffoldingwithController.Models;

namespace ScaffoldingwithController.Data
{
    public class ScaffoldingwithControllerContext : DbContext
    {
        public ScaffoldingwithControllerContext (DbContextOptions<ScaffoldingwithControllerContext> options)
            : base(options)
        {
        }

        public DbSet<TestModel> TestModel { get; set; }
    }
}
