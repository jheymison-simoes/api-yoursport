using YourSport.Api.Configuration;
using YourSport.Data;

namespace YourSport.Api;

public class Startup
{
    private IConfiguration Configuration { get; }
        
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApiConfiguration(Configuration);
        services.DependencyInjection(Configuration);
        services.AddSwaggerConfiguration();
        // services.AddMediatRConfig();
        // services.AddMessageBusConfiguration(Configuration);
        // services.AddAppTelemetryTracing();
        // services.AddHangFireConfig(Configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SqlContext context)
    {
        // if (AspnetcoreEnvironment.IsCurrentAspnetcoreEnvironmentValue(AspnetcoreEnvironmentEnum.Docker))
        // {
        //     context.Database.Migrate();
        // }
        
        //app.UseAppMetrics("schedulingApi", "Counts requests to the Mottu Zika User Managment Api Endpoints");
        app.UseApiConfiguration(env);
        app.UseSwaggerConfiguration();
        //app.UseHangfire();
    }
}
