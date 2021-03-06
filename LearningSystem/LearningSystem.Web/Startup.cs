﻿namespace LearningSystem.Web
{
    using AutoMapper;
    using Data;
    using Data.Models;
    using Helpers.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<LearningSystemDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                })
                .AddEntityFrameworkStores<LearningSystemDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication().AddFacebook(fo =>
            {
                fo.AppId = "544650015933761";
                fo.AppSecret = "87da7dd6cdf2ccd738237f213b208074";
            });

            services.AddAuthentication().AddGoogle(go =>
            {
                go.ClientId = "562286623453-brfn7el0m5u09hjmo25ml197l4o5drd1.apps.googleusercontent.com";
                go.ClientSecret = "_TdKF6dmhKI_Xf_ymTF9_S8T";
            });

            services.AddAutoMapper();

            services.AddDomainServices();

            services.AddRouting(routing => routing.LowercaseUrls = true);

            services.AddMvc(options =>
            {
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDatabaseMigration();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/home/error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "profile",
                    template: "users/{username}",
                    defaults: new { controller = "Users", action = "Profile" });

                routes.MapRoute(
                    name: "blog",
                    template: "blog/articles/{id}/{title}",
                    defaults: new { area = "Blog", controller = "Articles", action = "Details" });

                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
