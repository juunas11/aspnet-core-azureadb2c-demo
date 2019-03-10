namespace AspNetCoreBtoC
{
    public class AzureAdSettings
    {
        public string ClientId { get; set; }
        public string Tenant { get; set; }
        public string SignUpOrInPolicyId { get; set; }
        public string UserProfilePolicyId { get; set; }
        public string ForgotPwPolicyId { get; set; }
        public string AadInstance { get; set; }
        public string UserProfileCallbackPath { get; set; }
        public string SignUpOrInCallbackPath { get; set; }
        public string ForgotPwCallbackPath { get; set; }
    }
}
