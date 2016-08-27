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
            ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                loggerFactory.AddDebug(LogLevel.Information);
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseCookieAuthentication();

            app.UseAzureAdB2CAuthentication(new AzureAdB2CAuthenticationOptions
            {
                SignUpPolicyId = Configuration["AzureAd:SignUpPolicyId"],
                SignUpCallbackPath = Configuration["AzureAd:SignUpCallbackPath"],
                ForgotPasswordPolicyId = Configuration["AzureAd:ForgotPwPolicyId"],
                ForgotPasswordCallbackPath = Configuration["AzureAd:ForgotPwCallbackPath"],
                EditProfilePolicyId = Configuration["AzureAd:UserProfilePolicyId"],
                EditProfileCallbackPath = Configuration["AzureAd:ProfileCallbackPath"],
                SignInPolicyId = Configuration["AzureAd:SignInPolicyId"],
                SignInCallbackPath = Configuration["AzureAd:SignInCallbackPath"],
                SignUpOrInPolicyId = Configuration["AzureAd:SignUpOrInPolicyId"],
                SignUpOrInCallbackPath = Configuration["AzureAd:SignUpOrInCallbackPath"],
                AzureAdInstance = Configuration["AzureAd:AadInstance"],
                Tenant = Configuration["AzureAd:Tenant"],
                ClientId = Configuration["AzureAd:ClientId"],
                PostLogoutRedirectUri = Configuration["AzureAd:RedirectUri"]
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
