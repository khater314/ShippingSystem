using BL.Contracts;
using BL.Mapping;
using BL.Services;
using DAL.Contracts;
using DAL.DbContext;
using DAL.Repositories;
using DAL.UserModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using AppResources.Localization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using WebApi.Services;
using Microsoft.OpenApi;

namespace WebApi
{
    public static class RegisterServicesHelper
    {

        public static void RegisterServices(WebApplicationBuilder builder)
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
                //options.LoginPath = "/Account/Login";
                //options.AccessDeniedPath = "/Account/AccessDenied";
                //options.LogoutPath = "/Account/Logout";
                //options.ExpireTimeSpan = TimeSpan.FromMinutes(1500);
                //options.SlidingExpiration = true;
                //options.Cookie.HttpOnly = true;
                //options.Cookie.SameSite = SameSiteMode.Lax;
                //options.Cookie.Name = "Cookie";
                //options.ReturnUrlParameter = "ReturnUrl";
                //options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            });
            #endregion

            #region JWT Configuration
            var jwtKey = builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key is missing from configuration!");
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtKey)),
                    ClockSkew = TimeSpan.Zero
                };
            });
            #endregion

            #region Swagger Configuration

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Shipping API",
                    Description = "An ASP.NET Core Web API for managing shipping operations.",
                    //TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Ahmed Khater",
                        Email = "khaterx314@gmail.com",
                    }
                });
            });
            #endregion

            #region CORS Configuration
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("ShippingOnlyPolicy",
                    policy =>
                    {
                        policy.WithOrigins("https://localhost:7210")
                              .AllowAnyMethod()
                              .AllowAnyHeader()
                              .AllowCredentials();
                    });
            });
            #endregion

            builder.Services.AddAuthorization();

            builder.Services.AddAppLocalization();


            #region Logger Configuration

            Log.Logger = new LoggerConfiguration()
                            .ReadFrom.Configuration(builder.Configuration)
                            .CreateLogger();

            builder.Host.UseSerilog();
            #endregion


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
            builder.Services.AddScoped<IRateSettingService, RateSettingService>();

            builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            builder.Services.AddSingleton<TokenService>();

            builder.Services.AddScoped<BL.Contracts.IUserService, WebApi.Services.UserService>();
            // Mapping Add Scoped
            builder.Services.AddScoped<BL.Mapping.IMapper, BL.Mapping.AutoMapperAdapter>();
            // Filters Add Scoped
            builder.Services.AddScoped<Filters.TransactionExceptionFilter>();
            #endregion
        }
    }
}
