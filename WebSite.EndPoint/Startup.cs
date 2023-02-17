using AliShop.Application.Contexts.Interfaces;
using AliShop.Application.Visitors.SaveVisitorInfo;
using AliShop.Application.Visitors.VisitorOnLine;
using AliShop.Infrustructure.IdentityConfigs;
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
using WebSite.EndPoint.Hubs;
using WebSite.EndPoint.Utilities.Filters;

namespace WebSite.EndPoint
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
                  services.AddControllersWithViews();
                  string conection = Configuration["conectionString:sqlServer"];
                  services.AddDbContext<DataBaseContext>(option=>option.UseSqlServer(conection));
                  services.AddDbContext<IdentityDataBaseContext>(option=>option.UseSqlServer(conection));

                  services.AddIdentityService(Configuration);
                  services.AddAuthorization();
                  services.ConfigureApplicationCookie(option =>
                  {
                        option.ExpireTimeSpan= TimeSpan.FromMinutes(10);
                        option.LoginPath = "/account/login";
                        option.AccessDeniedPath= "/Account/AccessDenied";
                        option.SlidingExpiration = true;
                  });
            services.AddTransient(typeof(AliShop.Application.Contexts.Interfaces.IMongoDbContext<>),typeof(AliShop.Persistence.Contexts.MongoContext.MongoDbContext<>));  
            services.AddTransient<ISaveVisitorInfoService,SaveVisitorInfoService>();
            services.AddTransient<IVisitorOnLineService, VisitorOnLineService>();
            services.AddScoped<SaveVisitorFilter>();
            services.AddSignalR();
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
                  app.UseHttpsRedirection();
                  app.UseStaticFiles();

                  app.UseRouting();

                  app.UseAuthentication();
                  app.UseAuthorization();

                  app.UseEndpoints(endpoints =>
                  {
                        endpoints.MapControllerRoute(
                      name: "default",
                      pattern: "{controller=Home}/{action=Index}/{id?}");
                      endpoints.MapHub<OnLineVisitorHub>("/chathub");
                  });
            }
      }
}
