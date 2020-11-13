# Example ASP.NET Core web application
An ASP.NET Core web application using FusionAuth as the identity server.
This application will use an OAuth Authorization Code workflow
and the PKCE extension to log users in. PKCE stands for Proof
Key for Code Exchange, and is often pronounced “pixie”. The feature
is enabled by default by the netcore oauth library.

You need to have dotnetcore installed to run this code. If you
are using a mac, use this command `brew cask install dotnet-sdk` to setup the development
environment.

Set up fusionauth as documented in the blog post. 

To deploy and run on Windows, assuming you have the dotnetcore 3.1 runtime installed:

* Open up a terminal (these instructions assume a `cmd` window)
* `cd SampleApp`
* Update `appsettings.json` with the FusionAuth application `ClientId` and the `Authority` as necessary.
* Export the client secret as an environment variable: `set SampleApp__ClientSecret="..."`
* Publish the binary: `dotnet publish -r win-x64`
* Run the application: `bin\Debug\netcoreapp3.1\win-x64\publish\SampleApp.exe`

Visit the local webserver at `http://localhost:5000/` and sign in.

To run on a macos, use the [scripts here](https://dotnet.microsoft.com/download/dotnet-core/scripts) to install dotnetcore. Do the first 3 steps above.

Then run these commands instead of the last three:
* `export SampleApp__ClientSecret="..."`
* `dotnet publish -r osx.10.14-x64 -f net5.0`
* `bin/Debug/netcoreapp3.1/osx.10.14-x64/publish/SampleApp`

See more deployment options: https://docs.microsoft.com/en-us/dotnet/core/deploying/

See the blog post for more details about this code: [Securing an ASP.NET Core Razor Pages app with OAuth](https://fusionauth.io/blog/2020/05/06/securing-asp-netcore-razor-pages-app-with-oauth)

