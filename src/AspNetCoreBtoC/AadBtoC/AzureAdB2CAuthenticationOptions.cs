using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreBtoC.AadBtoC
{
    public class AzureAdB2CAuthenticationOptions
    {
        public string EditProfileCallbackPath { get; set; }
        public string EditProfilePolicyId { get; set; }
        public string ForgotPasswordCallbackPath { get; set; }
        public string ForgotPasswordPolicyId { get; set; }
        public string SignInCallbackPath { get; set; }
        public string SignInPolicyId { get; set; }
        public string SignUpCallbackPath { get; set; }
        public string SignUpOrInCallbackPath { get; set; }
        public string SignUpOrInPolicyId { get; set; }
        public string SignUpPolicyId { get; set; }
        public string AzureAdInstance { get; set; }
        public string Tenant { get; set; }
        public string ClientId { get; set; }
        public string PostLogoutRedirectUri { get; set; }
    }
}
