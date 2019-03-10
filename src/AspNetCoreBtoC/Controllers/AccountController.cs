using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace AspNetCoreBtoC.Controllers
{
    public class AccountController : Controller
    {
        private readonly AzureAdSettings _config;

        public AccountController(IOptions<AzureAdSettings> options)
        {
            _config = options.Value;
        }

        public IActionResult SignIn()
        {
            if (User.Identity.IsAuthenticated)
            {
                throw new Exception("Can't sign in, user logged in");
            }

            return Challenge(new AuthenticationProperties
            {
                RedirectUri = "/"
            },
            _config.SignUpOrInPolicyId);
        }

        public IActionResult EditProfile()
        {
            if (User.Identity.IsAuthenticated == false)
            {
                throw new Exception("Can't edit profile, user not logged in");
            }

            return Challenge(new AuthenticationProperties
            {
                RedirectUri = "/"
            },
            _config.UserProfilePolicyId);
        }

        public IActionResult ForgotPassword()
        {
            return Challenge(new AuthenticationProperties
            {
                RedirectUri = "/"
            },
            _config.ForgotPwPolicyId);
        }

        public IActionResult SignOut()
        {
            if (User.Identity.IsAuthenticated == false)
            {
                throw new Exception("Can't sign out, user not logged in");
            }

            string policyId = User.FindFirstValue("tfp");

            string returnUrl = Url.Action(
                action: nameof(SignedOut),
                controller: "Account",
                values: null,
                protocol: Request.Scheme);
            return SignOut(new AuthenticationProperties
            {
                RedirectUri = returnUrl
            },
            policyId,
            CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public IActionResult SignedOut()
        {
            return View();
        }
    }
}
