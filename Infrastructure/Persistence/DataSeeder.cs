using Domain.Entities;
using Domain.Enums;
using Domain.Interface.Common.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence;

public static class DataSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();
        var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

        // Ensure the database is created and migrations are applied
        await context.Database.MigrateAsync();

        // Idempotency check: If users already exist, skip the seeding process
        if (await context.Users.AnyAsync()) return;

        // Retrieve Admin credentials from Configuration (User Secrets / Env Variables)
        // Fallback to default values ONLY for Development environments
        var adminEmail = configuration["AdminSettings:Email"] ?? "admin@kiriblog.com";
        var adminPassword = configuration["AdminSettings:Password"] ?? "Admin123!";
        
        // Hardcoded Author for testing purposes (Optional)
        var authorEmail = "author@kiriblog.com";
        var authorPassword = "Author123!";
        

        var admin = User.CreateAdmin(
            adminEmail,
            "Kiri",      
            "Admin",
            passwordHasher.Hash(adminPassword)
        );


        var author = User.CreateAuthor(
            authorEmail,
            "Kiri",
            "Author",
            passwordHasher.Hash(authorPassword)
        );


        await context.Users.AddRangeAsync(admin, author);
        await context.SaveChangesAsync();
    }
}