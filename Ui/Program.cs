using BL.Contracts;
using BL.Mapping;
using BL.Services;
using DAL.Contracts;
using DAL.DbContext;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using AppResources.Localization;


namespace Ui
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            RegisterServicesHelper.RegisterServices(builder);

            // Add services to the container.
            builder.Services.AddControllersWithViews()
                .AddViewLocalization()
                .AddDataAnnotationsLocalization();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAppLocalization();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "Admin",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
