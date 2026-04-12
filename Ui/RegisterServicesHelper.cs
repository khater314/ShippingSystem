using BL.Contracts;
using BL.Mapping;
using BL.Services;
using DAL.Contracts;
using DAL.DbContext;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using AppResources.Localization;
using Microsoft.AspNetCore.Authentication.Cookies;
using DAL.UserModel;
using Microsoft.AspNetCore.Identity;

namespace Ui
{
    public static class RegisterServicesHelper
    {

        public static void RegisterServices(this WebApplicationBuilder builder)
        {
            #region Connection String
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ShippingContext>(options =>
                options.UseSqlServer(connectionString));
            #endregion

            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<ShippingContext>()
                .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.LogoutPath = "/Account/Logout";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(1500);
                options.SlidingExpiration = true;
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.Lax;
                options.Cookie.Name = "Cookie";
                options.ReturnUrlParameter = "ReturnUrl";
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            });

            builder.Services.AddAuthorization();

            builder.Services.AddAppLocalization();




            #region Dependency Injection
            builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile));
            /**********************************************************************/
            /***************************** Add Scoped *****************************/
            /**********************************************************************/
            // Repositories Add Scoped
            builder.Services.AddScoped(typeof(ITableRepository<>), typeof(TableRepository<>));
            builder.Services.AddScoped(typeof(IViewRepository<>), typeof(ViewRepository<>));
            // Services Add Scoped
            builder.Services.AddScoped<ICityService, CityService>();
            builder.Services.AddScoped<IShippingTypeService, ShippingTypeService>();
            builder.Services.AddScoped<IShipmentService, ShipmentService>();
            builder.Services.AddScoped<ICountryService, CountryService>();
            builder.Services.AddScoped<ICarrierService, CarrierService>();
            builder.Services.AddScoped<ISubscriptionPackageService, SubscriptionPackageService>();
            builder.Services.AddScoped<IUserContactService, UserContactService>();
            builder.Services.AddScoped<IUserSubscriptionService, UserSubscriptionService>();
            builder.Services.AddScoped<IPaymentMethodService, PaymentMethodService>();
            builder.Services.AddScoped<IShipmentStatusService, ShipmentStatusService>();
            builder.Services.AddScoped<ISettingService, SettingService>();

            builder.Services.AddScoped<BL.Contracts.IUserService, Ui.Services.UserService>();
            // Mapping Add Scoped
            builder.Services.AddScoped<BL.Mapping.IMapper, BL.Mapping.AutoMapperAdapter>();
            // Filters Add Scoped
            builder.Services.AddScoped<Filters.TransactionExceptionFilter>();
            #endregion
        }
    }
}
