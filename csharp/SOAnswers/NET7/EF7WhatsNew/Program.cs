using System.Net.Mime;
using EF7WhatsNew.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Hello, World!");


var serviceCollection = new ServiceCollection();
serviceCollection.AddDbContext<WhatsNewContext>(builder =>
    builder.UseNpgsql("Host=localhost;Port=6432;Database=whatsnewef_db;Username=postgres;Password=P@ssword")
        .LogTo(Console.WriteLine));
var serviceProvider = serviceCollection.BuildServiceProvider();

using (var scope = serviceProvider.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<WhatsNewContext>();
    // var migrator = ctx.GetInfrastructure().GetRequiredService<IMigrator>();
    //
    // migrator.Migrate();
    ctx.Database.EnsureDeleted();
    ctx.Database.EnsureCreated();

    ctx.SaveChanges();
}

using (var scope = serviceProvider.CreateScope())
{
}