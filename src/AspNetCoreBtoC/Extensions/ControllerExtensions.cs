using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Http.Features.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreBtoC.Extensions
{
    public static class ControllerExtensions
    {
        public static IActionResult Challenge(
            this Controller controller,
            AuthenticationProperties authenticationProperties,
            ChallengeBehavior challengeBehavior,
            params string[] authenticationSchemes)
        {
            return new MyChallengeResult(
                authenticationProperties,
                challengeBehavior,
                authenticationSchemes);
        }
    }
}
