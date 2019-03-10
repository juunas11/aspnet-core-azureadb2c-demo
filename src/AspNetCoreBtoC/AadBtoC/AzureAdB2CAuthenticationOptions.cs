namespace AspNetCoreBtoC.AadBtoC
{
    public class AzureAdB2CAuthenticationOptions
    {
        public string EditProfileCallbackPath { get; set; }
        public string EditProfilePolicyId { get; set; }
        public string ForgotPasswordCallbackPath { get; set; }
        public string ForgotPasswordPolicyId { get; set; }
        public string SignUpOrInCallbackPath { get; set; }
        public string SignUpOrInPolicyId { get; set; }
        public string AzureAdInstance { get; set; }
        public string Tenant { get; set; }
        public string ClientId { get; set; }
    }
}
