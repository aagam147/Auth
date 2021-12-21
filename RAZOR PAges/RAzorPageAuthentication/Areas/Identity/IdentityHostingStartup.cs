using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RAzorPageAuthentication.Data;

[assembly: HostingStartup(typeof(RAzorPageAuthentication.Areas.Identity.IdentityHostingStartup))]
namespace RAzorPageAuthentication.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<RAzorPageAuthenticationContext>(options =>
                    options.UseNpgsql(
                        context.Configuration.GetConnectionString("RAzorPageAuthenticationContextConnection")));

                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<RAzorPageAuthenticationContext>();
            });
        }
    }
}