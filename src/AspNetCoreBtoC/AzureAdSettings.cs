using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreBtoC
{
    public class AzureAdSettings
    {
        public string ClientId { get; set; }
        public string RedirectUri { get; set; }
        public string SignUpPolicyId { get; set; }
        public string SignInPolicyId { get; set; }
        public string UserProfilePolicyId { get; set; }
        public string SignUpOrInPolicyId { get; set; }
        public string ForgotPwPolicyId { get; set; }
        public string AadInstance { get; set; }
        public string Tenant { get; set; }
        public string SignInCallbackPath { get; set; }
        public string SignUpCallbackPath { get; set; }
        public string ProfileCallbackPath { get; set; }
        public string SignUpOrInCallbackPath { get; set; }
        public string ForgotPwCallbackPath { get; set; }
    }
}
