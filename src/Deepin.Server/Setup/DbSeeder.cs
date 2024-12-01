using Deepin.Server.Extensions;
using Deepin.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Deepin.Domain.UserAggregates;
using Deepin.Domain.RoleAggregates;
using Deepin.Domain.Entities;

namespace Deepin.Server.Setup;

public class DbSeeder(IServiceProvider serviceProvider) : IDbSeeder<DeepinDbContext>
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    public async Task SeedAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
        await SeedRolesAsync(roleManager);
        var admin = await SeedUsersAsync(userManager);
        await SeedCategoriesAsync(admin.Id.ToString(), scope.ServiceProvider.GetRequiredService<DeepinDbContext>());
    }
    private async Task SeedRolesAsync(RoleManager<Role> roleManager)
    {
        if (roleManager.Roles.Any())
        {
            return;
        }
        var roles = new List<Role>
        {
            new Role { Name = "Owner" },
            new Role { Name = "Admin" },
            new Role { Name = "User" }
        };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role.Name!))
            {
                await roleManager.CreateAsync(role);
            }
        }
    }
    private async Task<User> SeedUsersAsync(UserManager<User> userManager)
    {
        var user = await userManager.FindByNameAsync("admin");
        if (user != null)
        {
            return user;
        }
        user = new User
        {
            UserName = "leoyang@deepin.me",
            Email = "leoyang@deepin.me",
            EmailConfirmed = true
        };
        var result = await userManager.CreateAsync(user, "Password@123");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "Owner");
        }
        return user;
    }
    private async Task SeedCategoriesAsync(string userId, DeepinDbContext context)
    {
        if (!context.Categories.Any())
        {
            var categories = new List<Category>
            {
                new Category(userId, "Quick notes", "note", null,-20, true),
                new Category(userId, "Drafts", "drafts", null,-10, true),
                new Category(userId, "Todo List", "checklist", null,-1, true),
                new Category(userId, "Work", "work"),
                new Category(userId, "Personal", "personal"),
                new Category(userId, "Shopping", "shopping"),
                new Category(userId, "Travel", "travel"),
                new Category(userId, "Study", "study"),
                new Category(userId, "Health", "health"),
                new Category(userId, "Finance", "finance"),
                new Category(userId, "Others", "others"),
            };
            await context.Categories.AddRangeAsync(categories);
            await context.SaveChangesAsync();
        }
    }
}
