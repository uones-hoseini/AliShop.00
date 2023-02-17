using Admin.EndPoint.MappingProfiles;
using AliShop.Application.Catalogs.CatalogTypes.CrudService;
using AliShop.Application.Contexts.Interfaces;
using AliShop.Application.Visitors.GetTodayReport;
using AliShop.Infrustructure.IdentityConfigs.MappingProfile;
using AliShop.Persistence.Contexts;
using AliShop.Persistence.Contexts.MongoContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.EndPoint
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
            services.AddRazorPages();
            services.AddTransient<IGetTodayReportService, GetTodayReportService>();
            services.AddTransient(typeof(IMongoDbContext<>), typeof(MongoDbContext<>));
            services.AddTransient<ICatalogTypeService, CatalogTypeService>();


            #region connection String SqlServer
            services.AddScoped<IDataBaseContext, DataBaseContext>();
            string connection = Configuration["conectionString:sqlServer"];
            services.AddDbContext<DataBaseContext>(option => option.UseSqlServer(connection));
            #endregion

            //mapper
            services.AddAutoMapper(typeof(CatalogMappingProfile));
            services.AddAutoMapper(typeof(CatalogVMMappingProfile));
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
