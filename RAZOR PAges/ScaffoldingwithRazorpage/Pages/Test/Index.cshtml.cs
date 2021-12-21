using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CRUDOPERATION.Model;
using ScaffoldingwithRazorpage.Data;

namespace ScaffoldingwithRazorpage.Pages.Test
{
    public class IndexModel : PageModel
    {
        private readonly ScaffoldingwithRazorpage.Data.ScaffoldingwithRazorpageContext _context;

        public IndexModel(ScaffoldingwithRazorpage.Data.ScaffoldingwithRazorpageContext context)
        {
            _context = context;
        }

        public IList<Registration> Registration { get;set; }

        public async Task OnGetAsync()
        {
            Registration = await _context.Registration.ToListAsync();
        }
    }
}
