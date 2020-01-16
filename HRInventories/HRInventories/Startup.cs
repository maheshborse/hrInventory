using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRInventories.Models;
using HRInventories.Services;
using HRInventories.Services.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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
            AddDataAccessDI(services);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            ///services.AddEntityFrameworkNpgsql().AddDbContext<HRInventoryDBContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DatabaseConnection")));
            
            string connString = Configuration.GetSection("Connectionstrings").GetSection("DatabaseConnection").Value;
            Connectionstrings connectionString = new Connectionstrings() { DatabaseConnection = connString };
            services.AddDbContext<HRInventoryDBContext>(options => options.UseNpgsql(connString));
            services.AddSingleton(connectionString);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
        private void AddDataAccessDI(IServiceCollection services)
        {
            services.AddSingleton<IDataAccess, DataAccess>();
        }
    }
}
