using Deepin.Server.Infrastructure.Filters;
using Deepin.Server.Infrastructure.Services;
using Deepin.Server.Setup;
using Deepin.Application.Extensions;
using Deepin.Application.Services;
using Deepin.Domain.UserAggregates;
using Deepin.Infrastructure;
using Deepin.Infrastructure.Configurations;
using Deepin.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Deepin.Server.Extensions;

public static class WebApplicationExtensions
{
    private const string ALLOW_ANY_CORS_POLICY = "allow_any";
    public static WebApplicationBuilder AddApplicationService(this WebApplicationBuilder builder)
    {
        var appSettings = new AppSettings(
            dbConnection: builder.Configuration.GetConnectionString("POSTGRES") ?? throw new ArgumentNullException("DbConnection must be not null."),
            dataFolder: builder.Configuration["DATA_STORAGE_FOLDER"] ?? throw new ArgumentNullException("Data storage folder must be not null."),
            fileStorageType: Enum.Parse<FileStorageType>(builder.Configuration["DATA_STORAGE_TYPE"] ?? throw new ArgumentNullException("Data storage folder must be not null."), true),
            redisConnection: builder.Configuration.GetConnectionString("REDIS"));

        builder.Services
            .AddInfrastructure(appSettings)
            .AddApplication()
            .AddHttpContexts()
            .AddMigration<DeepinDbContext>((db, sp) => new DbSeeder(sp).SeedAsync())
            .AddAspNetCoreIdentity()
            .AddCustomMvc()
            .AddCustomCorsPolicy();
        return builder;
    }
    public static WebApplication ConfigureApplicationService(this WebApplication app)
    {
        app.UseDefaultFiles();
        app.MapStaticAssets();
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();

            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseCors(ALLOW_ANY_CORS_POLICY);
        app.MapGroup("identity").MapIdentityApi<User>();

        app.MapControllers();

        app.MapFallbackToFile("/index.html");

        return app;
    }
    internal static IServiceCollection AddAspNetCoreIdentity(this IServiceCollection services)
    {
        services
        .AddIdentityApiEndpoints<User>()
        .AddEntityFrameworkStores<DeepinDbContext>();

        services.Configure<IdentityOptions>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
            options.User.RequireUniqueEmail = true;
            options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
        });

        return services;
    }
    internal static IServiceCollection AddCustomMvc(this IServiceCollection services)
    {
        services
            .AddControllers(options =>
            {
                options.Filters.Add<HttpGlobalExceptionFilter>();
            })
            .AddNewtonsoftJson(options =>
            {
                var jsonSettings = options.SerializerSettings;
                jsonSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                jsonSettings.Converters = [
                    new StringEnumConverter(){ NamingStrategy = new CamelCaseNamingStrategy()}
                    ];
                jsonSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                jsonSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                JsonConvert.DefaultSettings = () => jsonSettings;
            });

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        services.AddOpenApi();
        services.AddSwaggerGen();
        return services;
    }
    internal static IServiceCollection AddCustomCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(ALLOW_ANY_CORS_POLICY, builder =>
            builder.SetIsOriginAllowed((host) => true).AllowAnyMethod().AllowAnyHeader().AllowCredentials());
        });
        return services;
    }
    internal static IServiceCollection AddHttpContexts(this IServiceCollection services)
    {

        services.AddHttpContextAccessor();
        services.AddTransient<IUserContext, HttpUserContext>();
        return services;
    }
}
