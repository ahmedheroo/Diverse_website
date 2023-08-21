using Diverse_website.Data;
using Diverse_website.Models;
using Diverse_website.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using ReflectionIT.Mvc.Paging;
using Diverse_website.Helpers;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Diverse_website
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
            //services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.AddMvc()
      .AddDataAnnotationsLocalization(options =>
      {
          options.DataAnnotationLocalizerProvider = (type, factory) =>
              factory.Create(typeof(Resource));
      });
            services.Configure<RequestLocalizationOptions>(option =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en"),
                    new CultureInfo("ar-EG")

                };
                option.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(culture: "en", uiCulture: "en");
                option.SupportedCultures = supportedCultures;
                option.SupportedUICultures = supportedCultures;
            });
            services.AddMvc(mvc =>
            {
                mvc.Conventions.Add(new ControllerNameAttributeConvention());
            });
            services.AddDbContext<Diverse_websiteContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredUniqueChars = 0;

            }).AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
            services.Configure<DataProtectionTokenProviderOptions>(o =>
        o.TokenLifespan = TimeSpan.FromMinutes(20));
            services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromHours(20);
            });
            services.AddAuthentication(o =>
            {
                o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                .AddCookie(o => o.LoginPath = "/identity/account/login");
            services.AddAuthorization(op => op.AddPolicy("AdminRole", p => p.RequireRole("admin")));

            //services.AddAuthentication( CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(q => q.LoginPath = "/identity/account/login");
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IBlogsRepo, BlogsRepo>();
            services.AddScoped<IProjectRepo, ProjectRepo>();
            services.AddScoped<IVendorsRepo, VendorsRepo>();
            services.AddScoped<ISearchRepo, SearchRepo>();
            services.AddTransient<IEmailSender, EmailSender>();
            //services.AddSingleton<IRazorViewEngine, RazorViewEngine>();
            //services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<IdentityUser> userManager)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.UseRequestLocalization(
           app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseStatusCodePagesWithRedirects("/Error/{0}");
                //app.UseExceptionHandler("/Shared/PageNotFound");
                //app.UseHsts();
            }
            // DBIntializer.seed(userManager);
            // DBIntializer.seedroles(userrole);
            app.UseForwardedHeaders();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            var lockOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(lockOptions.Value);
            app.UseAuthentication();
            app.UseAuthorization();
            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<Diverse_websiteContext>();
            // var Identitycontext = services.GetRequiredService<ApplicationDbContext>();
            var logger = services.GetRequiredService<ILogger<Startup>>();
            try
            {
                // Identitycontext.Database.MigrateAsync();
                context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {

                logger.LogError(ex, "Database Updating Error");
            }
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
