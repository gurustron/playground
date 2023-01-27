using EF7WhatsNew.Db.Blogginng;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EF7WhatsNew.Db.Animals;

public class TPCAnimalsRunner
{
    public static async Task Do()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddDbContext<AnimalsContext>(builder =>
            builder.UseNpgsql("Host=localhost;Port=6432;Database=animals_hierarchy_db;Username=postgres;Password=P@ssword")
                .LogTo(Console.WriteLine, LogLevel.Information));
        var serviceProvider = serviceCollection.BuildServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var ctx = scope.ServiceProvider.GetRequiredService<AnimalsContext>();
            await ctx.Database.EnsureDeletedAsync();
            Console.Clear();
            await ctx.Database.EnsureCreatedAsync();
            var arthur = new Human("Arthur") { };
            var wendy = new Human("Wendy");
            var christi = new Human("Christi");

            var alice = new Cat("Alice", "MBA") { Vet = "Pengelly", Humans = { arthur, wendy } };

            var mac = new Cat("Mac", "Preschool") { Vet = "Pengelly", Humans = { arthur, wendy } };

            var toast = new Dog("Toast", "Mr. Squirrel") { Vet = "Pengelly", Humans = { arthur, wendy } };

            var clyde = new FarmAnimal("Clyde", "Equus africanus asinus") { Value = 100.0m };

            wendy.FavoriteAnimal = toast;
            arthur.FavoriteAnimal = alice;
            christi.FavoriteAnimal = clyde;

            await ctx.AddRangeAsync(wendy, arthur, christi, alice, mac, clyde);
            ctx.Add(toast);
            await ctx.SaveChangesAsync();
        }

        Console.Clear();
        
        using (var scope = serviceProvider.CreateScope())
        {
            var ctx = scope.ServiceProvider.GetRequiredService<AnimalsContext>();
            var listAsync = await ctx.Animals.ToListAsync();
        }
    }
}