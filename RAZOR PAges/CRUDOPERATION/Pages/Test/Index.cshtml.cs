using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUDOPERATION.Data;
using CRUDOPERATION.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CRUDOPERATION.Pages.Test
{
    public class IndexModel : PageModel
    {
        private readonly CrudoperationDbContext _crudoperationDbContext;
        public IndexModel(CrudoperationDbContext crudoperationDbContext)
        {
            _crudoperationDbContext = crudoperationDbContext;
        }
        public IList<Registration> registration { get; set; }
        public async Task<IActionResult> OnGet()
        {
            registration= await _crudoperationDbContext.registrations.ToListAsync();
            return Page();
        }
        
    }
}
