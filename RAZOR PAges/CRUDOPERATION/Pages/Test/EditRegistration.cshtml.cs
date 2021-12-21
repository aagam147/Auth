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
    public class EditRegistrationModel : PageModel
    {
        private readonly CrudoperationDbContext _crudoperationDbContext;
        public EditRegistrationModel(CrudoperationDbContext crudoperationDbContext)
        {
            _crudoperationDbContext = crudoperationDbContext;
        }
       
        [BindProperty]
        public Registration Registration { get; set; }
        //public async Task<IActionResult> onPostAsync(int id, [Bind("Id", "Name", "Email", "BirthDate", "MobileNo", "Password", "ConfirMPassword")] Registration registration)
        //{
        //    if (id != registration.Id)
        //    {
        //        return NotFound();
        //    }
        //    //var testmodel = _crudoperationDbContext.registrations.FindAsync(id);

        //    _crudoperationDbContext.Update(registration);
        //    await _crudoperationDbContext.SaveChangesAsync();


        //    return Redirect("/Test/Index");
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id, [Bind("Id,Name,Email,BirthDate,MobileNo,Password,ConfirMPassword")] Registration registration)
        {

            if (id != registration.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _crudoperationDbContext.Update(registration);
               await _crudoperationDbContext.SaveChangesAsync();
                return Redirect("/Test/Index");
            }

            return Page();
          
        }

        private bool RegistrationExists(int id)
        {
            return _crudoperationDbContext.registrations.Any(e => e.Id == id);
        }
    }
}
