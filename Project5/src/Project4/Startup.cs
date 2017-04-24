using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Project5.Models;
using Newtonsoft.Json.Serialization;
using Project5.Repositories;
using Microsoft.AspNet.Authentication.Cookies;
using System.Net;

namespace Project5
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                //.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public static IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc().AddJsonOptions(opt =>
            {
                opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            services.AddIdentity<TodoUser, IdentityRole>(
                c =>
                {
                    c.Password.RequiredLength = 8;
                    c.User.RequireUniqueEmail = false;
                    c.Password.RequireNonLetterOrDigit = false;
                    c.Password.RequireUppercase = true;
                    c.Password.RequireDigit = true;
                    c.Cookies.ApplicationCookie.Events = new CookieAuthenticationEvents()
                    {
                        OnRedirectToLogin = ctx =>
                        {
                            if (ctx.Request.Path.StartsWithSegments("/api"))
                            {
                                ctx.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            }

                            return Task.FromResult(0);
                        }
                    };


                }).AddEntityFrameworkStores<TodoContext>();

            services.AddEntityFramework().AddSqlServer().AddDbContext<TodoContext>();
            services.AddTransient<TodoAppSeedData>();
            services.AddScoped<ITodoRepository, TodoRepository>();
            services.AddScoped<ISettingRepository, SettingRepository>();



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, TodoAppSeedData seeder)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseIISPlatformHandler();

            app.UseDefaultFiles();

            app.UseStaticFiles();

            app.UseIdentity();

            app.UseMvc();

            await seeder.SeedData();
        }

        /// Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
