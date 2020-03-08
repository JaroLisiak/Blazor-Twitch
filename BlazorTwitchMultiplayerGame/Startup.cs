using AspNet.Security.OAuth.Twitch;
using BlazorTwitchMultiplayerGame.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http;


namespace BlazorTwitchMultiplayerGame
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services
                .AddAuthentication(TwitchAuthenticationDefaults.AuthenticationScheme)
                .AddCookie()
                .AddTwitch(option =>
                    {
                        option.ClientId = Configuration["Twitch:ClientId"];
                        option.ClientSecret = Configuration["Twitch:ClientSecret"];
                        option.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                        option.ForceVerify = true;
                    }
                );

            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();


            services.AddHttpContextAccessor();
            services.AddScoped<HttpContextAccessor>();
            services.AddHttpClient();
            services.AddScoped<HttpClient>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
