using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRMSInventoryMangement.Models;

using InventoryDataAccess.Models;
using InventoryDataAccess.Services;
using InventoryDataAccess.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace HRMSInventoryMangement
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
            AddDataAccessDI(services);
            //services.AddSwaggerGen(c =>
            ////{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Core Api", Version = "v1", Description= "Swagger Core Api" });
            //});

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("CorsPolicy",
            //        builder => builder
            //          .AllowAnyOrigin()
            //          .AllowAnyMethod()
            //          .AllowAnyHeader()
            //    .Build());
            //});
            services.AddMvc();
            string connString = Configuration.GetSection("Connectionstrings").GetSection("DefaultConnection").Value;
            ConnectionString connectionString = new ConnectionString() { DefaultConnection = connString };
            services.AddEntityFrameworkNpgsql().AddDbContext<DataContext>(options => options.UseNpgsql(Configuration.GetConnectionString(connString)));
            services.AddSingleton(connectionString);
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
                app.UseExceptionHandler();
            }
            // Code to generate OpenAPI documentation for swagger
            // app.UseCors("CorsPolicy");
            // app.UseStatusCodePages();
            // //app.UseAuthentication();
            //// app.UseMvc();

            // app.UseSwagger();
            // app.UseSwaggerUI(c =>
            // {
            //     c.SwaggerEndpoint("/swagger/v1/swagger.json", "Core Api");
            // }
            // );
            // app.UseHttpsRedirection();
            // app.UseStaticFiles();

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Category}/{action=AddCategory}");
        });
        }
    private void AddDataAccessDI(IServiceCollection services)
    {
        services.AddSingleton<IDataAccess, DataAccess>();

    }

}
}
