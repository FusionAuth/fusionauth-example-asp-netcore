using System;
using System.Linq;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace SampleApp
{
    public class Startup
    {
 
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
var loggerFactory = LoggerFactory.Create(builder =>
{
     builder.AddConsole();                
});

ILogger logger = loggerFactory.CreateLogger<Startup>();
logger.LogInformation("Example log message");
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            // Configure your policies
            services.AddAuthorization(options =>
                options.AddPolicy("Registered",
                //policy => policy.RequireClaim("aud", Configuration["SampleApp:ClientId"])));
                policy => policy.RequireClaim("aud")));
            services.AddRazorPages();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "cookie";
                options.DefaultChallengeScheme = "oidc";
            })
                .AddCookie("cookie", options =>
                {
                    options.Cookie.Name = Configuration["SampleApp:CookieName"];
                })
                .AddOpenIdConnect("oidc", options =>
                {

options.Events = new OpenIdConnectEvents
    {

        OnTokenResponseReceived = ctx =>
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(ctx.TokenEndpointResponse.AccessToken);
        logger.LogInformation("here");
        logger.LogInformation("jsonToken: "+jsonToken);
            
        logger.LogInformation("jsonToken.Claims: "+JsonSerializer.Serialize(jsonToken.Claims));

            //jsonToken.Claims <--here you go, update the ctx.Principal if necessary.


            return Task.CompletedTask;
        },
        OnTokenValidated = ctx =>
        {
        logger.LogInformation("here2");
   foreach (Claim claim in ctx.Principal.Claims)  
   {  
      logger.LogInformation("CLAIM TYPE: " + claim.Type + "; CLAIM VALUE: " + claim.Value + ","+claim);  
   }  
        //logger.LogInformation("ctx.Principal: "+JsonSerializer.Serialize(ctx.Principal));


            return Task.CompletedTask;
        }
    };

                    options.Authority = Configuration["SampleApp:Authority"];
                    options.ClientId = Configuration["SampleApp:ClientId"];
                    options.ClientSecret = Configuration["SampleApp:ClientSecret"];
                    options.Scope.Add("openid");
        	    options.ClaimActions.Remove("aud");

                    options.ResponseType = "code";
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidAudience = Configuration["SampleApp:ClientId"]
                    };
                });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
	    IdentityModelEventSource.ShowPII = true;
        }
    }
}

