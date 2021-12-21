﻿using System;
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
    public class DetailsModel : PageModel
    {
        private readonly RAzorPageAuthentication.Data.ApplicationDbContext _context;

        public DetailsModel(RAzorPageAuthentication.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Student Student { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Student = await _context.students.FirstOrDefaultAsync(m => m.Id == id);

            if (Student == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
