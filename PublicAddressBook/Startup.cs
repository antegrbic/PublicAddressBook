using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PublicAddressBook.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using PublicAddressBook.Persistence.Repositories;
using PublicAddressBook.Domain.Repository;
using PublicAddressBook.Persistence;
using PublicAddressBook.Domain.Services;
using PublicAddressBook.Services;
using static PublicAddressBook.Hubs.ContactsHub;

namespace PublicAddressBook
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

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("contacts-api-in-memory");
            }
            );

            services.AddScoped<IContactRepository, ContactRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IContactService, ContactService>();

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

            app.UseAuthorization();

            app.UseSignalR(routes => routes.MapHub<ContactHub>("/api/contactHub"));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
