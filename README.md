# Demo ASP.NET Core 1.0 app using Azure AD B2C for authentication

This application shows an example of how Azure AD B2C can be used for
authentication in ASP.NET Core 1.0.

It should be noted that I have not yet fully implemented things like
error handling. If the user clicks on the button that says
"I forgot my password" in the sign up or in flow, it causes an
unhandled error.

## Setup

You will first need to have a B2C directory ready, with an app registered
and the various types of policies setup.

After cloning the repository, rename **appsettings.example.json**
to **appsettings.json**, and add values for ids and callback
paths for the different policies (+ client id etc.) from Azure AD B2C.