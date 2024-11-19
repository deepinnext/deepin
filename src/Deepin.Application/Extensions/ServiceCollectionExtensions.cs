using Deepin.Application.Behaviors;
using Deepin.Application.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Deepin.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(ServiceCollectionExtensions).Assembly;
        return services
             .AddMediatR(config =>
             {
                 config.RegisterServicesFromAssemblies(assembly);
                 config.AddOpenBehavior(typeof(LoggingBehavior<,>));
                 config.AddOpenBehavior(typeof(ValidationBehavior<,>));
             })
             .AddAutoMapper(assembly)
             .AddQueries();
    }

    private static IServiceCollection AddQueries(this IServiceCollection services)
    {
        services.AddScoped<IPostQueries, PostQueries>();
        services.AddScoped<ITagQueries, TagQueries>();
        services.AddScoped<ICategoryQueries, CategoryQueries>();
        return services;
    }
}
