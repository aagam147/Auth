using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TwoWayAuthenticationUsingGoogle.Data;

[assembly: HostingStartup(typeof(TwoWayAuthenticationUsingGoogle.Areas.Identity.IdentityHostingStartup))]
namespace TwoWayAuthenticationUsingGoogle.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<TwoWayAuthenticationUsingGoogleContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("TwoWayAuthenticationUsingGoogleContextConnection")));

                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<TwoWayAuthenticationUsingGoogleContext>();
            });
        }
    }
}