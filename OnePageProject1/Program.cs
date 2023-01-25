using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnePageProject1.DAL;
using OnePageProject1.Models;

namespace OnePageProject1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("MsSQL"));
            });




            var app = builder.Build();
            app.UseStaticFiles();

            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
              );

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=home}/{action=index}"
                );

            app.Run();
        }
    }
}