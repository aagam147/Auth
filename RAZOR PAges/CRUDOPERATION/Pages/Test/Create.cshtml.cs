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
    public class CreateModel : PageModel
    {
        private readonly CrudoperationDbContext _crudoperationDbContext;
        public CreateModel(CrudoperationDbContext crudoperationDbContext)
        {
            _crudoperationDbContext = crudoperationDbContext;
        }
        [BindProperty]
        public Registration registration { get; set; }
        public async Task<IActionResult> OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            _crudoperationDbContext.Add(registration);
            await _crudoperationDbContext.SaveChangesAsync();

            return Redirect("/Test/Index");
        }
    }
}
