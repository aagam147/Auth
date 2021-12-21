using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUDOPERATION.Data;
using CRUDOPERATION.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CRUDOPERATION.Pages.Test
{
    public class DetailModel : PageModel
    {
        private readonly CrudoperationDbContext _crudoperationDbContext;
        public DetailModel(CrudoperationDbContext crudoperationDbContext)
        {
            _crudoperationDbContext = crudoperationDbContext;
        }

       //[BindProperty]
        public Registration Registration { get; set; }
        //public async Task<IActionResult> OnGetAsync(int? id)
        //{
        //    if(id == null)
        //    {
        //        return NotFound();
        //    }
        //   var  testmodel = await _crudoperationDbContext.registrations.FindAsync(id);
        //    if(testmodel == null)
        //    {
        //        return NotFound();
        //    }
        //    return Page();
        //}
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Registration = await _crudoperationDbContext.registrations.FindAsync(id);

            if (Registration == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
