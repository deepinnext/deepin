using Deepin.Server.Extensions;
using Deepin.Infrastructure;

namespace Deepin.Server.Setup;

public class DbSeeder(IServiceProvider serviceProvider) : IDbSeeder<DeepinDbContext>
{
    public Task SeedAsync()
    {
        return Task.CompletedTask;
    }
}
