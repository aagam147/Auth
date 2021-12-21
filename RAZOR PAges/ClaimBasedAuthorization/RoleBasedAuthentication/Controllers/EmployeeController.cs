
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using RoleBasedAuthentication.Data;
using RoleBasedAuthentication.Models;

namespace RoleBasedAuthentication.Controllers
{
   
    public class EmployeeController : Controller
    {
        private readonly RoleBasedAuthenticationDbContext _registrationlogin;
        
        public EmployeeController(RoleBasedAuthenticationDbContext registrationlogin)
        {
            _registrationlogin = registrationlogin;
        }
        //public async Task<IActionResult> Index(string sortOrder, string searchString)
        //{
        //    ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        //    ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
        //    ViewData["CurrentFilter"] = searchString;
        //    var students = from s in _registrationlogin.registrations
        //                   select s;
        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        students = students.Where(s => s.Name.Contains(searchString));
        //                              // || s.FirstMidName.Contains(searchString));
        //    }

        //    switch (sortOrder)
        //    {
        //        case "name_desc":
        //            students = students.OrderByDescending(s => s.Name);
        //            break;
        //        case "Date":
        //            students = students.OrderBy(s => s.BirthDate);
        //            break;
        //        case "date_desc":
        //            students = students.OrderByDescending(s => s.BirthDate);
        //            break;
        //        default:
        //            students = students.OrderBy(s => s.Name);
        //            break;
        //    }
        //    return View(await _registrationlogin.registrations.ToListAsync());
        //}
        public async Task<IActionResult> Index(
    string sortOrder,
    string currentFilter,
    string searchString,
    int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var students = from s in _registrationlogin.registrationModels
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Name.Contains(searchString));
                                      // || s.FirstMidName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.Name);
                    break;
                
                default:
                    students = students.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<RegistrationModel>.CreateAsync(students.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
        public IActionResult Registration()
        {
            return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            if(id== null)
            {
                return NotFound();
            }
            var testModel = await _registrationlogin.registrationModels
                 .FindAsync(id);
            if (testModel == null)
            {
                return NotFound();
            }

            return View(testModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration([Bind("Id,Name,Email,Password")] RegistrationModel registrationModel)
        {
            if(ModelState.IsValid)
            {
                _registrationlogin.Add(registrationModel);
                await _registrationlogin.SaveChangesAsync();
                return RedirectToAction("Login","Employee");
            }
            return View();
        }


        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Username,Password")] LoginModel login)
        {
            if(ModelState.IsValid)
            {
                //var xy = await _registrationlogin.registrations.FindAsync(1, login.Email);
                var x = _registrationlogin.registrationModels.Any(x=>x.Email==login.Username && x.Password == login.Password);
                if (x)
                {

                    ClaimsIdentity identity = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Name, login.Username),
                       new Claim(ClaimTypes.Email, "abc@gmail.com")
                    }, CookieAuthenticationDefaults.AuthenticationScheme);

                    //identity.AddClaim(new Claim("Example", login.Username));
                    
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                   return RedirectToAction("Index", "Home");
                }
                
                    //ViewBag.error = "Please enter valide crdintial";
                    
                
            }
            return View();
        }

        public async Task<IActionResult> EditRegistration(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var registrationmodel = await _registrationlogin.registrationModels.FindAsync(id);
            if(registrationmodel == null)
            {
                return NotFound();
            }
            return View(registrationmodel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRegistration(int id,[Bind("Id,Name,Email,Password")] RegistrationModel registrationModel)
        {
            if(id != registrationModel.Id)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                _registrationlogin.Update(registrationModel);
                await _registrationlogin.SaveChangesAsync();
                return RedirectToAction("Login", "Employee");
            }
            return View();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testModel = await _registrationlogin.registrationModels
                .FindAsync(id);
            if (testModel == null)
            {
                return NotFound();
            }

            return View(testModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var testModel = await _registrationlogin.registrationModels.FindAsync(id);
            _registrationlogin.registrationModels.Remove(testModel);
            await _registrationlogin.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //return View();
        }
        public IActionResult Logout()
        {
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
