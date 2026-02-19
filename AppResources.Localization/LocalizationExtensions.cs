using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using Microsoft.AspNetCore.Localization;



namespace AppResources.Localization;

public static class LocalizationExtensions
{
    public static IServiceCollection AddAppLocalization(this IServiceCollection services)
    {
        services.AddLocalization(options => options.ResourcesPath = "Resources");


        return services;
    }

    public static IApplicationBuilder UseAppLocalization(this IApplicationBuilder app)
    {
        var supportedCultures = new[]
        {
            new CultureInfo("en"),
            new CultureInfo("ar")
        };

        app.UseRequestLocalization(new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture("ar"),
            SupportedCultures = supportedCultures,
            SupportedUICultures = supportedCultures
        });

        return app;
    }
}