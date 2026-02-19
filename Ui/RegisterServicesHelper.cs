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

            builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile));
            builder.Services.AddScoped(typeof(ITableRepository<>), typeof(TableRepository<>));
            builder.Services.AddScoped<ICityService, CityService>();
            builder.Services.AddScoped<BL.Mapping.IMapper, BL.Mapping.AutoMapperAdapter>();

        }
    }
}
