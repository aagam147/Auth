using RegistrationAndLogin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistrationAndLogin.Data;
using RegistrationAndLogin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace RegistrationAndLogin.Controllers
{
   
    public class TestController : Controller
    {
        private readonly RegistrationandLoginDbContext _registrationlogin;
        
        public TestController(RegistrationandLoginDbContext registrationlogin)
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

            var students = from s in _registrationlogin.registrations
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
                case "Date":
                    students = students.OrderBy(s => s.BirthDate);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.BirthDate);
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
            var testModel = await _registrationlogin.registrations
                 .FindAsync(id);
            if (testModel == null)
            {
                return NotFound();
            }

            return View(testModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration([Bind("Id,Name,Email,BirthDate,MobileNo,Password,ConfirMPassword")] RegistrationModel registrationModel)
        {
            if(ModelState.IsValid)
            {
                _registrationlogin.Add(registrationModel);
                await _registrationlogin.SaveChangesAsync();
                return RedirectToAction("Login","Test");
            }
            return View();
        }


        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email,Password")] LoginModel login)
        {
            if(ModelState.IsValid)
            {
                //var xy = await _registrationlogin.registrations.FindAsync(1, login.Email);
                var x = _registrationlogin.registrations.Any(x=>x.Email==login.Email);
                if (x)
                {
                    ClaimsIdentity identity = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Name, login.Email),
                        new Claim(ClaimTypes.Email, "Admin")
                    }, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                    return RedirectToAction("Index", "Test");
                }
                else {
                    ModelState.AddModelError("wrongEmail", "Please enter valide crdintial");
                    //ViewBag.error = "Please enter valide crdintial";
                    return View();
                }
            }
            return View();
        }

        public async Task<IActionResult> EditRegistration(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var registrationmodel = await _registrationlogin.registrations.FindAsync(id);
            if(registrationmodel == null)
            {
                return NotFound();
            }
            return View(registrationmodel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRegistration(int id,[Bind("Id,Name,Email,BirthDate,MobileNo,Password,ConfirMPassword")] RegistrationModel registrationModel)
        {
            if(id != registrationModel.Id)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                _registrationlogin.Update(registrationModel);
                await _registrationlogin.SaveChangesAsync();
                return RedirectToAction("Login", "Test");
            }
            return View();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testModel = await _registrationlogin.registrations
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
            var testModel = await _registrationlogin.registrations.FindAsync(id);
            _registrationlogin.registrations.Remove(testModel);
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
