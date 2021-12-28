using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoginWithExternal.Areas.Identity.Pages.Account.Manage
{
    public class EnableAuthenticatorModel : PageModel
    {
        public void OnGet()
        {
        }
        //private string GenerateQrCodeUri(string email, string unformattedKey)
        //{
        //    return string.Format(
        //        AuthenticatorUriFormat,
        //        _urlEncoder.Encode("Razor Pages"),
        //        _urlEncoder.Encode(email),
        //        unformattedKey);
        //}
    }
}
