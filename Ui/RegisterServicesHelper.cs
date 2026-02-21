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
    public static class RegisterServicesHelper
    {

        public static void RegisterServices(WebApplicationBuilder builder)
        {

            #region Connection String
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ShippingContext>(options =>
                options.UseSqlServer(connectionString));
            #endregion

            builder.Services.AddAppLocalization();

            #region Dependency Injection
            builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile));
            builder.Services.AddScoped(typeof(ITableRepository<>), typeof(TableRepository<>));
            //Services Add Scoped
            builder.Services.AddScoped<ICityService, CityService>();
            builder.Services.AddScoped<IShippingTypeService, ShippingTypeService>();
            builder.Services.AddScoped<IShippmentService, ShippmentService>();
            builder.Services.AddScoped<ICountryService, CountryService>();
            builder.Services.AddScoped<ICarrierService, CarrierService>();
            builder.Services.AddScoped<ISubscriptionPackageService, SubscriptionPackageService>();
            builder.Services.AddScoped<IUserReceiverService, UserReceiverService>();
            builder.Services.AddScoped<IUserSenderService, UserSenderService>();
            builder.Services.AddScoped<IUserSubscriptionService, UserSubscriptionService>();
            builder.Services.AddScoped<IPaymentMethodService, PaymentMethodService>();
            builder.Services.AddScoped<IShippmentStatusService, ShippmentStatusService>();
            builder.Services.AddScoped<BL.Mapping.IMapper, BL.Mapping.AutoMapperAdapter>();
            //Services Add Singleton
            builder.Services.AddScoped<ISettingService, SettingService>();
            #endregion
        }
    }
}
