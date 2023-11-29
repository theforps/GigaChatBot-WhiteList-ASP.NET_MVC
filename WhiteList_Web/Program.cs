using GigaChat_Bot.repositories;
using Microsoft.AspNetCore.Cors.Infrastructure;
using WhiteList_Web.Data.Impl;
using WhiteList_Web.Data.Interfaces;
using WhiteList_Web.Services;
using WhiteList_Web.Services.Interfaces;

namespace WhiteList
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {

                app.UseHsts();
            }



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