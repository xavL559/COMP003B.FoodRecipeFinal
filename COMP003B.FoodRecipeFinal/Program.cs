using COMP003B.FoodRecipeFinal.Data;
using Microsoft.EntityFrameworkCore;

namespace COMP003B.FoodRecipeFinal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<WebDevAcademyContext>(options =>
    options.UseSqlServer("Name=ConnectionStrings:DefaultConnection"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WebDevAcademyContext>(
                options => options.UseSqlServer(
                    "<connection string>",
                    providerOptions => providerOptions.EnableRetryOnFailure()));
        }
    }
}