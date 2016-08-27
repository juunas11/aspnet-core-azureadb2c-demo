using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Http.Features.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreBtoC.Extensions;
using Microsoft.Extensions.Options;

namespace AspNetCoreBtoC.Controllers
{
    public class AccountController : Controller
    {
        //private readonly IConfiguration config;
        private readonly AzureAdSettings config;

        public AccountController(IOptions<AzureAdSettings> options)
        {
            config = options.Value;
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
            config.SignInPolicyId);
        }

        public IActionResult SignUp()
        {
            if (User.Identity.IsAuthenticated)
            {
                throw new Exception("Can't sign up, user logged in");
            }

            return Challenge(new AuthenticationProperties
            {
                RedirectUri = "/"
            },
            config.SignUpPolicyId);
        }

        public IActionResult EditProfile()
        {
            if (User.Identity.IsAuthenticated == false)
            {
                throw new Exception("Can't edit profile, user not logged in");
            }

            return this.Challenge(new AuthenticationProperties
            {
                RedirectUri = "/"
            },
            ChallengeBehavior.Unauthorized,
            config.UserProfilePolicyId);
        }

        public IActionResult ForgotPassword()
        {
            return Challenge(new AuthenticationProperties
            {
                RedirectUri = "/"
            },
            config.ForgotPwPolicyId);
        }

        public IActionResult SignOut()
        {
            if (User.Identity.IsAuthenticated == false)
            {
                throw new Exception("Can't sign out, user not logged in");
            }

            string returnUrl = Url.Action(
                action: nameof(SignedOut),
                controller: "Account",
                values: null,
                protocol: Request.Scheme);
            return SignOut(new AuthenticationProperties
            {
                RedirectUri = returnUrl
            },
            config.ForgotPwPolicyId,
            config.SignUpOrInPolicyId,
            config.UserProfilePolicyId,
            config.SignUpPolicyId,
            config.SignInPolicyId,
            CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public IActionResult SignedOut()
        {
            return View();
        }
    }
}
