using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;

namespace Localication_GlobalizationApp
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            ////Add localization
            //services.AddLocalization(options =>
            //{
            //    options.ResourcesPath = "Resources";
            //});

            //services.AddMvc().AddViewLocalization(op => {
            //    op.ResourcesPath = "Resources";
            //}).AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
            //.AddDataAnnotationsLocalization()
            //    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //services.Configure<RequestLocalizationOptions>(op =>
            //{
            //    var supportedCulture = new List<CultureInfo>
            //    {
            //        new CultureInfo("en"),
            //        new CultureInfo("en-US")
            //        ,new CultureInfo("fr"),
            //        new CultureInfo("ru"),
            //        new CultureInfo("ja"),
            //        new CultureInfo("fr-FR"),
            //        new CultureInfo("zh-CN"),  //Chienese China
            //        new CultureInfo("ar-EG")     // Arabic Egypt
            //    };

            //    op.DefaultRequestCulture = new RequestCulture("en-SU");

            //    //Formeting Number, Dates, etc

            //    op.SupportedCultures = supportedCulture;

            //    // UI string that   we have Localize

            //    op.SupportedCultures = supportedCulture;
            //});

            services.AddLocalization(opts => {
                opts.ResourcesPath = "Resources";
            });

            services.AddMvc()
                    .AddViewLocalization(opts => { opts.ResourcesPath = "Resources"; })
                    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                    .AddDataAnnotationsLocalization()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.Configure<RequestLocalizationOptions>(opts => {
                var supportedCultures = new List<CultureInfo> {
                    new CultureInfo("en"),
                    new CultureInfo("en-US"),
                    new CultureInfo("fr"),
                    new CultureInfo("ru"),
                    new CultureInfo("ja"),
                    new CultureInfo("fr-FR"),
                    new CultureInfo("zh-CN"),   // Chinese China
                    new CultureInfo("ar-EG"),   // Arabic Egypt
                  };

                opts.DefaultRequestCulture = new RequestCulture("en-US");
                // Formatting numbers, dates, etc.
                opts.SupportedCultures = supportedCultures;
                // UI strings that we have localized.
                opts.SupportedUICultures = supportedCultures;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            //var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            //app.UseRequestLocalization(options.Value);

            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);


            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
