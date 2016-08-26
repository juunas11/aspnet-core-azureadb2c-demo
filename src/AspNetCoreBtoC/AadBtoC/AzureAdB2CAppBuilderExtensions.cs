using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreBtoC.AadBtoC
{
    public static class AzureAdB2CAppBuilderExtensions
    {
        public static IApplicationBuilder UseAzureAdB2CAuthentication(
            this IApplicationBuilder app,
            AzureAdB2CAuthenticationOptions options)
        {
            if(app == null)
            {
                throw new ArgumentNullException(nameof(app), "Application builder can't be null");
            }
            if(options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if(options.SignUpPolicyId != null && options.SignUpCallbackPath != null)
            {
                app.UseOpenIdConnectAuthentication(
                    CreateOidConnectOptionsForPolicy(
                        options.SignUpPolicyId,
                        options.SignUpCallbackPath,
                        options,
                        automaticChallenge: false));
            }
            if(options.ForgotPasswordPolicyId != null && options.ForgotPasswordCallbackPath != null)
            {
                app.UseOpenIdConnectAuthentication(
                    CreateOidConnectOptionsForPolicy(
                        options.ForgotPasswordPolicyId,
                        options.ForgotPasswordCallbackPath,
                        options,
                        automaticChallenge: false));
            }
            if(options.EditProfilePolicyId != null && options.EditProfileCallbackPath != null)
            {
                app.UseOpenIdConnectAuthentication(
                    CreateOidConnectOptionsForPolicy(
                        options.EditProfilePolicyId,
                        options.EditProfileCallbackPath,
                        options,
                        automaticChallenge: false));
            }
            if(options.SignInPolicyId != null && options.SignInCallbackPath != null)
            {
                app.UseOpenIdConnectAuthentication(
                    CreateOidConnectOptionsForPolicy(
                        options.SignInPolicyId,
                        options.SignInCallbackPath,
                        options,
                        automaticChallenge: false));
            }
            if(options.SignUpOrInPolicyId != null && options.SignUpOrInCallbackPath != null)
            {
                app.UseOpenIdConnectAuthentication(
                    CreateOidConnectOptionsForPolicy(
                        options.SignUpOrInPolicyId,
                        options.SignUpOrInCallbackPath,
                        options,
                        automaticChallenge: true));
            }

            return app;
        }

        private static OpenIdConnectOptions CreateOidConnectOptionsForPolicy(
            string policyId,
            string callbackPath,
            AzureAdB2CAuthenticationOptions options,
            bool automaticChallenge)
        {
            if(options.AzureAdInstance == null)
            {
                throw new ArgumentNullException("options.AzureAdInstance");
            }
            if(options.Tenant == null)
            {
                throw new ArgumentNullException("options.Tenant");
            }
            if(options.PostLogoutRedirectUri == null)
            {
                throw new ArgumentNullException("options.PostLogoutRedirectUri");
            }
            if(options.ClientId == null)
            {
                throw new ArgumentNullException("options.ClientId");
            }

            var opts = new OpenIdConnectOptions
            {
                AuthenticationScheme = policyId,
                MetadataAddress = string.Format(options.AzureAdInstance, options.Tenant, policyId),
                ClientId = options.ClientId,
                PostLogoutRedirectUri = options.PostLogoutRedirectUri,
                ResponseType = "id_token",
                TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = "name"
                },
                CallbackPath = callbackPath,
                AutomaticChallenge = automaticChallenge
            };

            opts.Scope.Add("openid");

            return opts;
        }
    }
}
