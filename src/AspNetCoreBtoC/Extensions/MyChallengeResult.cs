using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Http.Features.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreBtoC.Extensions
{
    internal class MyChallengeResult : IActionResult
    {
        private readonly AuthenticationManager authenticationManager;
        private readonly AuthenticationProperties authenticationProperties;
        private readonly string[] authenticationSchemes;
        private readonly ChallengeBehavior challengeBehavior;

        public MyChallengeResult(AuthenticationManager authenticationManager, AuthenticationProperties authenticationProperties, ChallengeBehavior challengeBehavior, string[] authenticationSchemes)
        {
            this.authenticationProperties = authenticationProperties;
            this.challengeBehavior = challengeBehavior;
            this.authenticationSchemes = authenticationSchemes;
            this.authenticationManager = authenticationManager;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            foreach (string scheme in authenticationSchemes)
            {
                await authenticationManager.ChallengeAsync(scheme, authenticationProperties, challengeBehavior);
            }
        }
    }
}