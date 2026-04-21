using BL.Contracts;
using BL.Mapping;
using BL.Services;
using DAL.Contracts;
using DAL.DbContext;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using AppResources.Localization;
using Ui.Filters;
using Microsoft.AspNetCore.Identity;
using DAL.UserModel;
using Ui.Services;

//Test Realease at GitHub
namespace Ui
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //RegisterServicesHelper.RegisterServices(builder);
            builder.RegisterServices();

            // Add services to the container.
            builder.Services.AddControllersWithViews(option =>
            {
                option.Filters.Add<TransactionExceptionFilter>();
            }).AddViewLocalization().AddDataAnnotationsLocalization();

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
                .RequireAuthorization()
                .WithStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .RequireAuthorization()
                .WithStaticAssets();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                var shippingContext = services.GetRequiredService<ShippingContext>();

                await shippingContext.Database.MigrateAsync();

                await ContextConfig.SeedDataAsync(shippingContext, roleManager, userManager);
            }

            app.Run();
        }
    }
}
