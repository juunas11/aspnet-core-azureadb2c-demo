using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using AspNetCoreBtoC.AadBtoC;
using Microsoft.Extensions.Options;

namespace AspNetCoreBtoC
{
    public class Startup
    {
        private IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(env.ContentRootPath)
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();

            services.Configure<AzureAdSettings>(Configuration.GetSection("AzureAd"));

            services.AddMvc();
            services.AddAuthentication(
                opts => opts.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            IOptions<AzureAdSettings> aadSettings)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                loggerFactory.AddDebug(LogLevel.Information);
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseCookieAuthentication();

            var aadConfig = aadSettings.Value;

            app.UseAzureAdB2CAuthentication(new AzureAdB2CAuthenticationOptions
            {
                SignUpPolicyId = aadConfig.SignUpPolicyId,
                SignUpCallbackPath = aadConfig.SignUpCallbackPath,
                ForgotPasswordPolicyId = aadConfig.ForgotPwPolicyId,
                ForgotPasswordCallbackPath = aadConfig.ForgotPwCallbackPath,
                EditProfilePolicyId = aadConfig.UserProfilePolicyId,
                EditProfileCallbackPath = aadConfig.UserProfileCallbackPath,
                SignInPolicyId = aadConfig.SignInPolicyId,
                SignInCallbackPath = aadConfig.SignInCallbackPath,
                SignUpOrInPolicyId = aadConfig.SignUpOrInPolicyId,
                SignUpOrInCallbackPath = aadConfig.SignUpOrInCallbackPath,
                AzureAdInstance = aadConfig.AadInstance,
                Tenant = aadConfig.Tenant,
                ClientId = aadConfig.ClientId,
                PostLogoutRedirectUri = aadConfig.RedirectUri
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "Default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
