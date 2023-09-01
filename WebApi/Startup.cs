using System;
using DataAccess.EFCore.Data;
using DataAccess.EFCore.Repositories;
using DataAccess.EFCore.UnitOfWorks;
using Domain.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebApi
{
    public class Startup
    {
        public static string hrefURL { get; private set; }
        public static string ExcelConString2 { get; private set; }
        public static string BaseURLAcclimate { get; private set; }
        public static string BaseUrl { get; private set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            hrefURL = Configuration.GetSection("hrefURL").Value;
            BaseUrl = Configuration.GetSection("BaseUrl").Value;
            BaseURLAcclimate = Configuration.GetSection("BaseURLAcclimate").Value;
            ExcelConString2 = Configuration.GetSection("ExcelConString2").Value;
        }
        public static string GethrefURL()
        {
            return Startup.hrefURL;
        }

        public static string GetExcelConString2()
        {
            return Startup.ExcelConString2;
        }
        public static string GetBaseUrl()
        {
            return Startup.BaseUrl;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCors(); // Make sure you call this previous to AddMvc
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddDbContext<db_cubicall_game_devContext>(opts => opts.UseMySql(Configuration.GetConnectionString("CubicallDB")));
            #region Repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //services.AddTransient<IDeveloperRepository, DeveloperRepository>();
            //services.AddTransient<IProjectRepository, ProjectRepository>();
            #endregion
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(x => x
               .AllowAnyMethod()
               .AllowAnyHeader()
               .SetIsOriginAllowed(origin => true) // allow any origin
               .AllowCredentials()); // allow credentials
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
