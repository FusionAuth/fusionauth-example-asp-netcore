# Example ASP.NET Core web application
An ASP.NET Core web application using FusionAuth as the identity server.

You need to have dotnetcore installed to run this code.

Set up fusionauth as documented in the blog post. 

To deploy and run:

* Open up a terminal (these instructions assume a `cmd` window)
* `cd SampleApp`
* update `appsettings.json` with the FusionAuth application `ClientId`
* Export the client secret as an environment variable: `set SampleApp__ClientSecret="..."`
* publish the binary: `dotnet publish -r win-x64`
* `bin\Debug\netcoreapp3.1\win-x64\publish\SampleApp.exe`

Visit the local webserver at `http://localhost:5000/` and sign in.

See more deployment options: https://docs.microsoft.com/en-us/dotnet/core/deploying/

See the blog post for more details about this code: [Securing an ASP.NET Core Razor Pages app with OAuth](https://fusionauth.io/blog/2020/05/06/securing-asp-netcore-razor-pages-app-with-oauth)

