using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using ProjectIDGenerator.Data;
using Microsoft.EntityFrameworkCore;
using ProjectIDGenerator.Areas.Identity;

[assembly: HostingStartup(typeof(IdentityHostingStartup))]
namespace ProjectIDGenerator.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices(((context, services) =>
            {
                //IConfiguration configuration = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
                //var mySecretValue = configuration["ConnectionStrings:DefaultConnection"];
                services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));
                IdentityEntityFrameworkBuilderExtensions.AddEntityFrameworkStores<ApplicationDbContext>(
                services.AddDefaultIdentity<ApplicationUser>(
                  options =>
                  {
                      /* options.SignIn.RequireConfirmedAccount = false;*/
                      // Password settings.
                      options.Password.RequireDigit = true;
                      options.Password.RequireLowercase = true;
                      options.Password.RequiredUniqueChars = 1;
                      options.Password.RequireUppercase = true;
                      /*                          options.Password.RequiredUniqueChars = 1;
                      */
                      // Lockout settings.
                      options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                      options.Lockout.MaxFailedAccessAttempts = 5;
                      /*                          options.Lockout.AllowedForNewUsers = true;
                      */
                      // User settings.
                      options.User.AllowedUserNameCharacters =
                      "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@";
                      options.User.RequireUniqueEmail = false;
                  })
                .AddRoles<IdentityRole>()
                //.AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders()
                );
                services.Configure<DataProtectionTokenProviderOptions>(options =>
                options.TokenLifespan = TimeSpan.FromDays(1));
                services.AddDistributedMemoryCache();
                services.AddSession(options =>
                {
                    options.IdleTimeout = TimeSpan.FromMinutes(30);
                    options.Cookie.HttpOnly = true;
                    options.Cookie.IsEssential = true;
                });
                services.ConfigureApplicationCookie(options =>
                {
                    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                    options.Cookie.HttpOnly = true;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                    options.LoginPath = "/Identity/Account/Login";
                    // ReturnUrlParameter requires 
                    //using Microsoft.AspNetCore.Authentication.Cookies;
                    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                    options.SlidingExpiration = true;
                });
            }));
        } 

    }
}
