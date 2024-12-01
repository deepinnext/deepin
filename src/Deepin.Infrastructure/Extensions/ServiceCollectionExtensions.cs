using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Deepin.Infrastructure.Configurations;
using Deepin.Infrastructure.FileStorage;
using Deepin.Infrastructure.BlobStorage;
using Deepin.Domain.PostAggregates;
using Deepin.Infrastructure.Repositories;
using Deepin.Domain.NoteAggregates;

namespace Deepin.Infrastructure.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, AppSettings appSettings)
    {
        return services
             .AddSingleton(appSettings)
             .AddDbContexts(appSettings)
             .AddRepository()
             .AddCaching(appSettings)
             .AddStorage(appSettings);
    }
    private static IServiceCollection AddDbContexts(this IServiceCollection services, AppSettings appSettings)
    {
        services.AddDbContext<DeepinDbContext>(options =>
        {
            options.UseNpgsql(appSettings.DbConnection, sql =>
            {
                sql.EnableRetryOnFailure(3);
            });
        }, ServiceLifetime.Scoped);
        return services;
    }
    private static IServiceCollection AddRepository(this IServiceCollection services)
    {
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<INoteRepository, NoteRepository>();
        return services;
    }
    private static IServiceCollection AddCaching(this IServiceCollection services, AppSettings appSettings)
    {
        if (appSettings.UseRedisCache)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = appSettings.RedisConnection;
            });
        }
        else
        {
            services.AddMemoryCache();
        }
        return services;
    }
    public static IServiceCollection AddStorage(this IServiceCollection services, AppSettings appSettings)
    {
        switch (appSettings.FileStorageType)
        {
            case FileStorageType.FileSystem:
                services.AddSingleton<IFileStorage, LocalFileStorage>();
                break;
            default:
                throw new NotSupportedException("Not support file storage type.");
        }
        return services;
    }
}
