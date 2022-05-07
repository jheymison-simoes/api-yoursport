using System.Globalization;
using System.Resources;
using YourSport.Api.Resource;
using YourSport.Data.Configuration;

namespace YourSport.Api.Configuration;

public static class DependencyInjectionConfig
{
    public static IServiceCollection DependencyInjection(this IServiceCollection services,
        IConfiguration configuration)
    {

        services.AddSingleton(new ResourceManager(typeof(YourSportResource)));
        services.AddScoped(provider =>
        {
            var httpContext = provider.GetService<IHttpContextAccessor>()?.HttpContext;

            if (httpContext == null)
            {
                return CultureInfo.InvariantCulture;
            }

            return httpContext.Request.Headers.TryGetValue("language", out var language)
                ? new CultureInfo(language)
                : CultureInfo.InvariantCulture;
        });

        services.DependencyInjection();
        
        // BusinessDependencyInjectionConfig.DependencyInjection(services);
        // DataDependencyInjectionConfig.DependencyInjection(services, configuration);
        // ApplicationDependencyInjectionConfig.DependencyInjection(services);
   
        return services;
    }
}