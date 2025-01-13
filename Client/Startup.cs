using Client.Base;
using Client.Repositories.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;

namespace Client
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
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10);//set 10 menit   
            });
            

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
            services.AddControllersWithViews().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddScoped<Address>();
            services.AddScoped<CourseRepository>();
            services.AddScoped<ContentRepository>();
            services.AddScoped<SubContentRepository>();
            services.AddScoped<ResourceRepository>();
            services.AddScoped<CategoryRepository>();
            services.AddScoped<CatalogueRepository>();
            services.AddScoped<EnrollmentRepository>();
            //services.AddHttpContextAccessor();
            services.AddScoped<AccountRepository>();
            services.AddScoped<UserRepository>();
            services.AddScoped<AuthorityRepository>();
            services.AddScoped<EnrollmentRepository>();
            services.AddScoped<ReviewRepository>();
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

            app.UseStatusCodePages(context => {
                var request = context.HttpContext.Request;
                var response = context.HttpContext.Response;

                if (response.StatusCode.Equals((int)HttpStatusCode.Forbidden))
                {
                    response.Redirect("/Login?login=false");
                }
                else if (response.StatusCode.Equals((int)HttpStatusCode.Unauthorized))
                {
                    response.Redirect("/Login?login=false");
                }
                else if (response.StatusCode.Equals((int)HttpStatusCode.NotFound))
                {
                    response.Redirect("/Admin/Notfound");
                }

                return Task.CompletedTask;
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.Use(async (context, next) =>
            {
                var JWToken = context.Session.GetString("JWToken");
                if (!string.IsNullOrEmpty(JWToken))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + JWToken);
                }
                await next();
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            /*app.UseSpa(spa =>

            {

                // To learn more about options for serving an Angular SPA from ASP.NET Core,

                // see https://go.microsoft.com/fwlink/?linkid=864501



                spa.Options.SourcePath = "ClientApp";



                if (env.IsDevelopment())

                {

                    spa.UseAngularCliServer(npmscript: "start");

                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");

                }

            });*/
        }
    }
}
