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


// using Newtonsoft.Json;
// using System.Text;
// using System.Resources;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.ApiExplorer;
// using Swashbuckle.AspNetCore.SwaggerUI;
// using Microsoft.OpenApi.Models;
// using Swashbuckle.AspNetCore.SwaggerGen;
// using Microsoft.AspNetCore.Authorization;
// using AutoMapper;
// using Microsoft.AspNetCore.Diagnostics.HealthChecks;
// using System.Globalization;
// using YourSport.Api.Configuration;
// using YourSport.Api.Resource;
// using YourSport.Data;
// using YourSport.Data.HealthCheck;
//
//
// namespace YourSport.Api;
//
// public class Startup
// {
//     public Startup(IConfiguration configuration)
//         {
//             Configuration = configuration;
//         }
//
//         readonly string AllowSpecificOrigins = "_AllowSpecificOrigins";
//
//         public IConfiguration Configuration { get; }
//
//         // This method gets called by the runtime. Use this method to add services to the container.
//         public void ConfigureServices(IServiceCollection services)
//         {
//             services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
//
//             services.AddCors(options =>
//             {
//                 options.AddPolicy(AllowSpecificOrigins,
//                 builder =>
//                 {
//                     builder.
//                         AllowAnyOrigin().
//                         AllowAnyHeader().
//                         AllowAnyMethod();
//                 });
//             });
//
//             services.AddApiVersioning(p =>
//             {
//                 p.DefaultApiVersion = new ApiVersion(1, 0);
//                 p.ReportApiVersions = true;
//                 p.AssumeDefaultVersionWhenUnspecified = true;
//             });
//
//             services.AddVersionedApiExplorer(p =>
//             {
//                 p.GroupNameFormat = "'v'VVV";
//                 p.SubstituteApiVersionInUrl = true;
//             });
//             
//             services.AddSwaggerGen(c =>
//             {
//                 c.SwaggerDoc("V1",
//                     new Microsoft.OpenApi.Models.OpenApiInfo
//                     {
//                         Title = "Your Sport API - V1",
//                         Version = "V1",
//                         Description = "Your Sport API",
//                     });
//
//                 // c.OperationFilter<AddAuthHeaderOperationFilter>();
//                 // c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
//                 // {
//                 //     Description = "`Token apenas!!!` - sem o prefixo `Bearer_`",
//                 //     In = ParameterLocation.Header,
//                 //     Type = SecuritySchemeType.Http,
//                 //     BearerFormat = "JWT",
//                 //     Scheme = "bearer"
//                 // });
//             });
//
//             var appSettingsSection = Configuration.GetSection("AppSettings");
//             var conStr = appSettingsSection.GetConnectionString("sqlserver");
//
//             services.AddDbContext<SqlContext>(options =>
//             {
//                 options
//                     .UseNpgsql(Configuration.GetConnectionString("DefaultConnection"),
//                         builder => builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery));
//             });
//
//
//             services.AddHealthChecks()
//                 .AddCheck(
//                     "Postgree Server",
//                     new HealthCheckDataBase(services.BuildServiceProvider()!.GetService<SqlContext>()!)
//                 );
//
//             services.Configure<AppSettings>(appSettingsSection);
//
//             services.AddMvc()
//                 .AddNewtonsoftJson();
//
//             var appSettings = appSettingsSection.Get<AppSettings>();
//
//             // var key = Encoding.ASCII.GetBytes(appSettings.JwtSecret);
//             // services.AddAuthentication(x =>
//             // {
//             //     x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//             //     x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//             // })
//             // .AddJwtBearer(x =>
//             // {
//             //     x.RequireHttpsMetadata = false;
//             //     x.SaveToken = true;
//             //     x.TokenValidationParameters = new TokenValidationParameters
//             //     {
//             //         ValidateIssuerSigningKey = true,
//             //         IssuerSigningKey = new SymmetricSecurityKey(key),
//             //         ValidateIssuer = false,
//             //         ValidateAudience = false
//             //     };
//             // });
//
//             services.AddSingleton(new ResourceManager(typeof(YourSportResource)));
//             services.AddScoped(provider =>
//             {
//                 var httpContext = provider.GetService<IHttpContextAccessor>()?.HttpContext;
//
//                 if (httpContext == null)
//                 {
//                     return CultureInfo.InvariantCulture;
//                 }
//
//                 return httpContext.Request.Headers.TryGetValue("language", out var language)
//                     ? new CultureInfo(language)
//                     : CultureInfo.InvariantCulture;
//             });
//             
//             
//
//             // services.DataInjectionConfiguration();
//             // services.ResolveDependeciesBusiness();
//             // services.ResolveDependeciesFactorys();
//
//             services.AddAutoMapper(typeof(Startup));
//
//             //services.AddWkhtmltopdf();
//         }
//
//         // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
//         public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
//         {
//             app.UseHealthChecks("/",
//                 new HealthCheckOptions
//                 {
//                     ResponseWriter = async (httpContext, healthReport) =>
//                     {
//                         var healthReportJson = JsonConvert.SerializeObject(healthReport);
//                         await httpContext.Response.WriteAsync(
//                             healthReportJson, Encoding.UTF8);
//                     }
//                 });
//             if (env.IsDevelopment())
//             {
//                 app.UseDeveloperExceptionPage();
//             }
//
//             app.UseSwagger();
//             app.UseSwaggerUI(c =>
//             {
//                 foreach (var description in provider.ApiVersionDescriptions)
//                 {
//                     c.SwaggerEndpoint(
//                    $"/swagger/{description.GroupName}/swagger.json",
//                    description.GroupName.ToUpperInvariant());
//                 }
//                 c.DocExpansion(DocExpansion.List);
//             });
//
//             app.UseRouting();
//
//
//             app.UseAuthentication();
//
//             app.UseAuthorization();
//
//             // app.UseKissLogMiddleware(options =>
//             // {
//             //     options.Listeners.Add(new RequestLogsApiListener(new MediaTypeNames.Application(
//             //         Configuration["KissLogOrganizationId"],
//             //         Configuration["KissLogApplicationId"])
//             //     )
//             //     {
//             //         Parser = new CustomListenerParser()
//             //     });
//             //
//             //     options.Options.GetUser((RequestProperties request) =>
//             //     {
//             //         return new UserDetails
//             //         {
//             //             Name = request.Claims.FirstOrDefault(f => f.Key == "id").Value
//             //         };
//             //     });
//             // });
//
//             app.UseCors(AllowSpecificOrigins);
//
//             app.UseEndpoints(endpoints =>
//             {
//                 endpoints.MapHealthChecks("/health/startup");
//                 endpoints.MapHealthChecks("/health/live", new HealthCheckOptions { Predicate = _ => false });
//                 endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions { Predicate = _ => false });
//                 endpoints.MapControllers();
//             });
//
//         }
//
//         private class AddAuthHeaderOperationFilter : IOperationFilter
//         {
//             public void Apply(OpenApiOperation operation, OperationFilterContext context)
//             {
//                 var isAuthorized = (context.MethodInfo.DeclaringType!.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any()
//                                     && !context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<AllowAnonymousAttribute>().Any()) //this excludes controllers with AllowAnonymous attribute in case base controller has Authorize attribute
//                                     || (context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any()
//                                     && !context.MethodInfo.GetCustomAttributes(true).OfType<AllowAnonymousAttribute>().Any()); // this excludes methods with AllowAnonymous attribute
//
//                 if (!isAuthorized) return;
//
//                 operation.Responses.TryAdd("401", new OpenApiResponse { Description = "Unauthorized" });
//                 operation.Responses.TryAdd("403", new OpenApiResponse { Description = "Forbidden" });
//
//                 var jwtbearerScheme = new OpenApiSecurityScheme
//                 {
//                     Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearer" }
//
//                 };
//
//                 operation.Security = new List<OpenApiSecurityRequirement>
//             {
//                 new OpenApiSecurityRequirement { [jwtbearerScheme] = new string []{} }
//             };
//             }
//         }
//
// }
