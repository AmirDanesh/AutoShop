using AutoShopping.Models;
using AutoShopping.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PersianTranslation.Identity;

namespace AutoShopping
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
            services.AddControllersWithViews();



            services.AddTransient<AutoShoppingDbContext>();
            services.AddTransient<IAutoRepository, AutoRepository>();

            services.AddAuthentication()
                .AddGoogle(option =>
                {
                    option.ClientId = "542168574928-flfh1hu346cek2nipcaia07f28qffris.apps.googleusercontent.com";
                    option.ClientSecret = "DkYwyp5OvZQ8BSkRlFnZFn38";
                });

            //for Using Identity
            services.AddIdentity<IdentityUser, IdentityRole>(option =>
            {
                option.User.RequireUniqueEmail = true;
                option.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                option.Password.RequireNonAlphanumeric = false;
                option.Lockout.MaxFailedAccessAttempts = 3;
            })
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<AutoShoppingDbContext>()
                .AddErrorDescriber<PersianIdentityErrorDescriber>();

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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //for Using Identity
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
