using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RoleBasedAuthentication.Models;
using RoleBasedAuthentication.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace RoleBasedAuthentication
{
    public class RegisterationNameHandler : AuthorizationHandler<RegisterationAuth>
    {
        private readonly RoleBasedAuthenticationDbContext _Dbcontext;

        public RegisterationNameHandler(RoleBasedAuthenticationDbContext roleBasedAuthenticationDbContext)
        {
            _Dbcontext = roleBasedAuthenticationDbContext;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext _Dbcontext,
                                                   RegisterationAuth requirement)
        {

            RegistrationModel Reg = this._Dbcontext.registrationModels.FirstOrDefault(x => x.Email == requirement.Email);
            //if (context.Resource != AuthorizationFilterContext redirectContext)
            //redirectContext.Result = new RedirectResult("/Employee/Login");
           // var mvcContext = _Dbcontext.Resource as Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext;
            //mvcContext.Result = new RedirectToActionResult("Login", "Employee", null);
            if (requirement.Email == Reg.Email)
            {
                _Dbcontext.Succeed(requirement);
            }
            else
            {
                _Dbcontext.Fail();
            }
            return Task.CompletedTask;
        }
    }
}
