using AppResources.Localization;
using BL.Contracts;
using BL.Mapping;
using BL.Services;
using DAL.Contracts;
using DAL.DbContext;
using DAL.Repositories;
using DAL.UserModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Ui.Services;

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

            #region Identity Configuration
            builder.Services.AddIdentity<AppUser, AppRole>(options =>
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
            #endregion

            #region Cookie Configuration
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
            #endregion

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
            });

            builder.Services.AddAppLocalization();

            #region Logger Configuration

            Log.Logger = new LoggerConfiguration()
                            .ReadFrom.Configuration(builder.Configuration)
                            .CreateLogger();

            builder.Host.UseSerilog();
            #endregion

            #region Dependency Injection

            builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile));

            #region Repositories
            builder.Services.AddScoped(typeof(ITableRepository<>), typeof(TableRepository<>));
            builder.Services.AddScoped(typeof(IViewRepository<>), typeof(ViewRepository<>));
            #endregion

            #region Tables & Views Services
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
            builder.Services.AddScoped<IRateSettingService, RateSettingService>();
            builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();

            builder.Services.AddScoped<BL.Contracts.IUserService, Ui.Services.UserService>();
            builder.Services.AddScoped<BL.Mapping.IMapper, BL.Mapping.AutoMapperAdapter>();
            builder.Services.AddScoped<Filters.TransactionExceptionFilter>();
            #endregion

            #region HttpClient Configuration
            var baseUrl = builder.Configuration.GetValue<string>("ApiSettings:BaseUrl");

            builder.Services.AddHttpClient("ShippingApiClient", client =>
            {
                client.BaseAddress = new Uri(baseUrl 
                    ?? throw new InvalidOperationException("Base URL is missing!"));

                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.Timeout = TimeSpan.FromMinutes(120);
            });

            builder.Services.AddScoped<GenericApiClient>();
            #endregion

            #endregion
        }
    }
}
