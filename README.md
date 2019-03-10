# Demo ASP.NET Core 2.1 app using Azure AD B2C for authentication

This application shows an example of how Azure AD B2C can be used for
authentication in ASP.NET Core 2.1.

It should be noted that I have not yet fully implemented things like
error handling.

## Setup

You will first need to have a B2C directory ready, with an app registered
and the various types of policies setup.

After cloning the repository, change values in appsettings.json for ids and callback
paths for the different policies (+ client id etc.) from Azure AD B2C.