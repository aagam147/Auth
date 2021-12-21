using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RAzorPageAuthentication.Data;
using RAzorPageAuthentication.Models;

namespace RAzorPageAuthentication.Pages.Students
{
    
    public class IndexModel : PageModel
    {
        private readonly RAzorPageAuthentication.Data.ApplicationDbContext _context;

        public IndexModel(RAzorPageAuthentication.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Student> Student { get;set; }

        public async Task OnGetAsync()
        {
            Student = await _context.students.ToListAsync();
        }

        
    }
}
