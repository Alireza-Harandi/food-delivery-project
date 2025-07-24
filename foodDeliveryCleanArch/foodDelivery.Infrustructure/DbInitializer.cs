using foodDelivery.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace foodDelivery.Infrustructure;

public static class DbInitializer
{
    public static void SeedAdminUser(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbManager = scope.ServiceProvider.GetRequiredService<DbManager>();

        if (!dbManager.Users.Any(u => u.Role == Role.Admin))
        {
            dbManager.Users.Add(
                new User("a1", "p1", Role.Admin)
            );
            dbManager.SaveChanges();
        }
    }
}