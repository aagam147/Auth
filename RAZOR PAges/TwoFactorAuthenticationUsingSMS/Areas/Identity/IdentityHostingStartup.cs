using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TwoFactorAuthenticationUsingSMS.Data;

[assembly: HostingStartup(typeof(TwoFactorAuthenticationUsingSMS.Areas.Identity.IdentityHostingStartup))]
namespace TwoFactorAuthenticationUsingSMS.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<TwoFactorAuthenticationUsingSMSContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("TwoFactorAuthenticationUsingSMSContextConnection")));

                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<TwoFactorAuthenticationUsingSMSContext>();
            });
        }
    }
}