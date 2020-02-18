using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TinyClothes.Data;


namespace TinyClothes
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private void ConfigDbContext(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("con goes here");
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages().AddRazorRuntimeCompilation();


            IMvcBuilder builder = services.AddControllersWithViews();

            //services.AddDbContext<StoreContext>(ConfigDbContext);
            string connection = Configuration.GetConnectionString("ClothesDB");
            //Same as above using lambda notation
            services.AddDbContext<StoreContext>
            (
                //NEED TO DO
                options => options.UseSqlServer(connection)
            );

            services.AddDistributedMemoryCache();

            //Add session and configures session
            services.AddSession(options =>
            {
                options.Cookie.Name = ".TinyClothes.Session";
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                //Session cook always get created even if user does not except cookie policy
                options.Cookie.IsEssential = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseBrowserLink();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
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

            app.UseAuthorization();

            //Allows session data to be accessed
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
