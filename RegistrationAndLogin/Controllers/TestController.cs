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
using Google.Authenticator;

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
        public async Task<IActionResult> Registration([Bind("Id,Name,Email,BirthDate,MobileNo,Password,ConfirMPassword")] RegistrationModel registrationModel)
        {
            if (ModelState.IsValid)
            {
                _registrationlogin.Add(registrationModel);
                await _registrationlogin.SaveChangesAsync();
                return RedirectToAction("Login", "Test");
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
            if (ModelState.IsValid)
            {
                //var userLogin= _registrationlogin.logins.OrderBy(x => x.Id).Where(x => x.Email == login.Email).LastOrDefault(); // TODO: fetch signed in user from a database
                //var xy = await _registrationlogin.registrations.FindAsync(1, login.Email);
                var x = _registrationlogin.registrations.Any(x => x.Email == login.Email && x.Password == login.Password);
                if (x)
                {

                    LoginModel user1=_registrationlogin.logins.OrderBy(x => x.Id).Where(x => x.Email == login.Email).LastOrDefault();
                    // LoginModel user = _registrationlogin.logins.FirstOrDefault();
                    if (user1 is null) {
                            _registrationlogin.Add(login);
                            _registrationlogin.SaveChangesAsync();
                    }
                    LoginModel user = _registrationlogin.logins.OrderBy(x => x.Id).Where(x => x.Email == login.Email).LastOrDefault();
                    ClaimsIdentity identity = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Name, login.Email),
                        new Claim(ClaimTypes.Email, "Admin")
                    }, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                    if (user.TwoFactorEnabled)
                    {
                    //    ClaimsIdentity identity = new ClaimsIdentity(new[] {
                    //    new Claim(ClaimTypes.Name, login.Email),
                    //    new Claim(ClaimTypes.Email, "Admin")
                    //}, CookieAuthenticationDefaults.AuthenticationScheme);
                    //    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                        return RedirectToAction("Index", "Test");
                    }
                }
                else {
                    ModelState.AddModelError("wrongEmail", "Please enter valide crdintial");
                    //ViewBag.error = "Please enter valide crdintial";
                    return View();
                }
            }
            return RedirectToAction("Qrcode","Test",login);
        }

        public async Task<IActionResult> EditRegistration(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var registrationmodel = await _registrationlogin.registrations.FindAsync(id);
            if (registrationmodel == null)
            {
                return NotFound();
            }
            return View(registrationmodel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRegistration(int id, [Bind("Id,Name,Email,BirthDate,MobileNo,Password,ConfirMPassword")] RegistrationModel registrationModel)
        {
            if (id != registrationModel.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
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

        [HttpGet]
        public ActionResult Qrcode([Bind("Email,Password")] LoginModel login,string Email)
        {
            LoginModel user = _registrationlogin.logins.OrderBy(x=>x.Id).Where(x=> x.Email == Email).LastOrDefault() ; // TODO: fetch signed in user from a database
            TwoFactorAuthenticator twoFactor = new TwoFactorAuthenticator();
            var setupInfo =
                twoFactor.GenerateSetupCode("RegistrationAndLogin", user.Email, TwoFactorKey(user), false, 3);
            ViewBag.SetupCode = setupInfo.ManualEntryKey;
            ViewBag.BarcodeImageUrl = setupInfo.QrCodeSetupImageUrl;
            return View();
        }

        private static string TwoFactorKey(LoginModel user)
        {
            return $"myverysecretkey+{user.Email}";
        
        }

        [HttpPost]
        public ActionResult Enable(string inputCode,string Email)
        {
            LoginModel user = _registrationlogin.logins.OrderBy(x=>x.Id).Where(x=> x.Email == Email).LastOrDefault() ; // TODO: fetch signed in user from a database
            TwoFactorAuthenticator twoFactor = new TwoFactorAuthenticator();
            bool isValid = twoFactor.ValidateTwoFactorPIN(TwoFactorKey(user), inputCode);
            if (!isValid)
            {
                return Redirect("/Test/Qrcode");
            }

            user.TwoFactorEnabled = true;
             _registrationlogin.Update(user);
             _registrationlogin.SaveChangesAsync();

            // TODO: store the updated user in database
            return Redirect("/");
        }
        [HttpGet]
        public IActionResult Disable()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Disable(string inputCode,string Email)
        {
            LoginModel user = _registrationlogin.logins.OrderBy(x => x.Id).Where(x => x.Email == Email).LastOrDefault(); // TODO: fetch signed in user from a database
            TwoFactorAuthenticator twoFactor = new TwoFactorAuthenticator();
            bool isValid = twoFactor.ValidateTwoFactorPIN(TwoFactorKey(user), inputCode);
            if (!isValid)
            {

                return RedirectToAction("Qrcode", "Test", user);
            }

            user.TwoFactorEnabled = false;
            _registrationlogin.Update(user);
            _registrationlogin.SaveChangesAsync();
            // TODO: store the updated user in database
            return RedirectToAction("Qrcode", "Test",user);
        }
    }
}
