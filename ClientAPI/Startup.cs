using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ClientAPI.Bootstrapper;
using ClientAPI.DataAccess;
using ClientAPI.DataAccess.Contracts;
using ClientAPI.DataAccess.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ClientAPI
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
            services.AddDbContext<DataContext> (db => db.UseSqlServer 
            (Configuration.GetConnectionString ("DefaultConnection")));
            
            services.AddAutoMapper();

            services.AddScoped(typeof(IPersonRepo), typeof(PersonRepo));
            
            services
            .AddIdentity<Models.ApplicationUser, IdentityRole>(
                x => {
                                       
                    x.Password.RequireUppercase = false;                    
                    x.Password.RequireLowercase = false;
                    x.Password.RequireDigit = false;
                    x.Password.RequiredLength = 4;
                    x.Password.RequireNonAlphanumeric = false;
                    x.SignIn.RequireConfirmedEmail = false;
                    x.SignIn.RequireConfirmedPhoneNumber = false;
                })
            .AddEntityFrameworkStores<DataContext>()
            .AddDefaultTokenProviders();
            
            services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer (options => 
            {                    
            
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;

                var key = Configuration.GetSection ("Jwt:Key").Value;

                var keyByteArray = System.Text.Encoding.ASCII.GetBytes (key);
                
                var signingKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(keyByteArray);

                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {        
               
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey =   signingKey,
                    ValidIssuer = "Issuer",
                    ValidAudience = "Audience"     

                };

            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("ApiPolicy", policy =>
                {
                    policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole("Admin","Member"); 

                });               

            });

            services.AddCors (options => {
                options.AddPolicy ("AllowAll",
                 p => {
                    p.AllowAnyOrigin ()
                        .AllowAnyHeader ()
                        .AllowAnyMethod ()
                        .AllowCredentials ();
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider sPovider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            // new RoleSeeder(sPovider).SeedRoles().Wait();
            // app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseMvc();

        
        }
    }
}
