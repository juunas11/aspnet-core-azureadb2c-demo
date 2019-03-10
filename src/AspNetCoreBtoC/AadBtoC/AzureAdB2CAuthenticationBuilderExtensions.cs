using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;

namespace AspNetCoreBtoC.AadBtoC
{
    public static class AzureAdB2CAuthenticationBuilderExtensions
    {
        public static AuthenticationBuilder AddAzureAdB2CAuthentication(
            this AuthenticationBuilder builder,
            AzureAdB2CAuthenticationOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (options.ForgotPasswordPolicyId != null && options.ForgotPasswordCallbackPath != null)
            {
                builder.AddOpenIdConnect(options.ForgotPasswordPolicyId, o =>
                    ConfigureOidConnectOptionsForPolicy(
                        o,
                        options.ForgotPasswordPolicyId,
                        options.ForgotPasswordCallbackPath,
                        options));
            }

            if (options.EditProfilePolicyId != null && options.EditProfileCallbackPath != null)
            {
                builder.AddOpenIdConnect(options.EditProfilePolicyId, o =>
                    ConfigureOidConnectOptionsForPolicy(
                        o,
                        options.EditProfilePolicyId,
                        options.EditProfileCallbackPath,
                        options));
            }

            if (options.SignUpOrInPolicyId != null && options.SignUpOrInCallbackPath != null)
            {
                builder.AddOpenIdConnect(options.SignUpOrInPolicyId, o =>
                    ConfigureOidConnectOptionsForPolicy(
                        o,
                        options.SignUpOrInPolicyId,
                        options.SignUpOrInCallbackPath,
                        options));
            }

            return builder;
        }

        private static void ConfigureOidConnectOptionsForPolicy(
            OpenIdConnectOptions options,
            string policyId,
            string callbackPath,
            AzureAdB2CAuthenticationOptions b2cOptions)
        {
            if (b2cOptions.AzureAdInstance == null)
            {
                throw new ArgumentNullException("options.AzureAdInstance");
            }

            if (b2cOptions.ClientId == null)
            {
                throw new ArgumentNullException("options.ClientId");
            }

            if (b2cOptions.Tenant == null)
            {
                throw new ArgumentNullException("options.Tenant");
            }

            options.Authority = string.Format(b2cOptions.AzureAdInstance, b2cOptions.Tenant, policyId);
            options.CallbackPath = callbackPath;
            options.ClientId = b2cOptions.ClientId;
            // The signout callback path needs to be unique for the policy
            // These don't need to be in the Reply URLs also
            options.SignedOutCallbackPath = callbackPath + "-signout";
            options.SignedOutRedirectUri = "/Account/SignedOut";
            options.TokenValidationParameters = new TokenValidationParameters
            {
                NameClaimType = "name"
            };
        }
    }
}
