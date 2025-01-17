﻿using System;
using IdentityProject.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(IdentityProject.Areas.Identity.IdentityHostingStartup))]
namespace IdentityProject.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<IdentityProjectContext>(options =>
                    options.UseNpgsql(
                        context.Configuration.GetConnectionString("IdentityProjectContextConnection")));

                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<IdentityProjectContext>();
    //            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    //.AddCookie();

            });
        }
    }
}