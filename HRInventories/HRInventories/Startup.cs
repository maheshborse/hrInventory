using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRInventories.Models;
using HRInventories.Services;
using HRInventories.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;

namespace HRInventories
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

            services.AddAuthentication(AzureADDefaults.BearerAuthenticationScheme).
                AddAzureADBearer(options => Configuration.Bind("AzureAd", options));

            //AddDataAccessDI(services);
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //.AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        ValidIssuer = Configuration["Jwt:Issuer"],
            //        ValidAudience = Configuration["Jwt:Issuer"],
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
            //    };
            //});

            // services.AddEntityFrameworkNpgsql().AddDbContext<HRInventoryDBContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DatabaseConnection")));

            //services.AddDbContext<HRInventoryDBContext>(options =>
            //options.UseSqlServer(Configuration.GetConnectionString("DatabaseConnections")));
            var qa = Configuration.GetSection("ConnectionStrings").GetSection("DatabaseName").Value;
            var con = Configuration.GetSection("ConnectionStrings").GetSection("DatabaseConnections").Value;
            var con1 = Configuration.GetSection("ConnectionStrings").GetSection("DatabaseConnection").Value;
            if (qa == "sql")
            {
                string connString = con;
                Connectionstrings connectionString = new Connectionstrings() { DatabaseConnections = connString, DatabaseName = qa, DatabaseConnection = connString};
                services.AddDbContext<HRInventoryDBContext>(options => options.UseSqlServer(connString));
                services.AddSingleton(connectionString);
            }
            else
            {
                string connString1 = con1;
                Connectionstrings connectionString1 = new Connectionstrings() { DatabaseConnection = connString1 };
                services.AddDbContext<HRInventoryDBContext>(options => options.UseNpgsql(connString1));
                services.AddSingleton(connectionString1);

            }

            //services.AddDbContext<HRInventoryDBContext>(options => options.UseSqlServer(connString));

            services.Configure<LdapConfig>(Configuration.GetSection("Ldap"));
            services.AddScoped<IAuthenticationRepository, LdapAuthenticationManager>();
            services.AddScoped<ICatagoryDataAccess, CatagoryDataAccess>();
            services.AddScoped<IdispatchDataAccess, dispatchDataAccess>();
            services.AddScoped<IPomasterDataAccess, PomasterDataAccess>();
            services.AddScoped<IProductDataAccess, ProductDataAccess>();
            services.AddScoped<IRequestDataAccess, RequestDataAccess>();


            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = JWTSettings.ValidIssuer,

                ValidateAudience = true,
                ValidAudience = JWTSettings.ValidAudience,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = JWTSettings.SigningKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = JWTSettings.ValidIssuer;
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                      .AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials()
                .Build());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Core Api", Description = "Swagger Core Api" });

            });
            services.AddMvc()
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //services.AddSingleton(connectionString);
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("CorsPolicy",
            //        builder => builder
            //          .AllowAnyOrigin()
            //          .AllowAnyMethod()
            //          .AllowAnyHeader()
            //          .AllowCredentials()
            //    .Build());
            //});
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Core Api", Description = "Swagger Core Api" });

            //    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            //    {
            //        Description =
            //            "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
            //        Name = "Authorization",
            //        In = ParameterLocation.Header,
            //        Type = SecuritySchemeType.ApiKey,
            //        Scheme = "Bearer"
            //    });
            //    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            //    {
            //        {
            //            new OpenApiSecurityScheme
            //            {
            //                Reference = new OpenApiReference
            //                {
            //                    Type = ReferenceType.SecurityScheme,
            //                    Id = "Bearer"
            //                },
            //                Scheme = "oauth2",
            //                Name = "Bearer",
            //                In = ParameterLocation.Header,

            //            },
            //            new List<string>()
            //        }
            //    });

            //});
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseStatusCodePages();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "Core Api");
            }
            );
            app.UseAuthentication();
            app.UseCookiePolicy();
            app.UseMvc();
        }
        private void AddDataAccessDI(IServiceCollection services)
        {
            services.AddSingleton<ICatagoryDataAccess, CatagoryDataAccess>();
            services.AddSingleton<IProductDataAccess, ProductDataAccess>();
            services.AddSingleton<IPomasterDataAccess, PomasterDataAccess>();
            services.AddSingleton<IdispatchDataAccess, dispatchDataAccess>();
        }
    }
}
