using WhiteList_Web.Data.Impl;
using WhiteList_Web.Data.Interfaces;

namespace WhiteList
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {

                app.UseHsts();
            }

            builder.Services.AddScoped<IUserRepository, UserRepository>();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Login}/{id?}");

            app.Run();
        }
    }
}