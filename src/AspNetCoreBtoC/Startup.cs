using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using AspNetCoreBtoC.AadBtoC;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreBtoC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.Configure<AzureAdSettings>(Configuration.GetSection("AzureAd"));
            var aadConfig = Configuration.GetSection("AzureAd").Get<AzureAdSettings>();

            services.AddAuthentication(o =>
            {
                o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = aadConfig.SignUpOrInPolicyId;
                o.DefaultForbidScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(o =>
            {
                o.Cookie.Name = "AspNetCoreBtoC.Auth";
            })
            .AddAzureAdB2CAuthentication(new AzureAdB2CAuthenticationOptions
            {
                ForgotPasswordPolicyId = aadConfig.ForgotPwPolicyId,
                ForgotPasswordCallbackPath = aadConfig.ForgotPwCallbackPath,
                EditProfilePolicyId = aadConfig.UserProfilePolicyId,
                EditProfileCallbackPath = aadConfig.UserProfileCallbackPath,
                SignUpOrInPolicyId = aadConfig.SignUpOrInPolicyId,
                SignUpOrInCallbackPath = aadConfig.SignUpOrInCallbackPath,
                AzureAdInstance = aadConfig.AadInstance,
                Tenant = aadConfig.Tenant,
                ClientId = aadConfig.ClientId
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();
        }
    }
}
