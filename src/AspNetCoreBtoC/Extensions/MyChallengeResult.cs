using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Http.Features.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreBtoC.Extensions
{
    public class MyChallengeResult : IActionResult
    {
        private readonly AuthenticationProperties authenticationProperties;
        private readonly string[] authenticationSchemes;
        private readonly ChallengeBehavior challengeBehavior;

        public MyChallengeResult(
            AuthenticationProperties authenticationProperties,
            ChallengeBehavior challengeBehavior,
            string[] authenticationSchemes)
        {
            this.authenticationProperties = authenticationProperties;
            this.challengeBehavior = challengeBehavior;
            this.authenticationSchemes = authenticationSchemes;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            AuthenticationManager authenticationManager =
                context.HttpContext.Authentication;

            foreach (string scheme in authenticationSchemes)
            {
                await authenticationManager.ChallengeAsync(
                    scheme,
                    authenticationProperties,
                    challengeBehavior);
            }
        }
    }
}