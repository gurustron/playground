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
    ctx.Database.EnsureDeleted();
    ctx.Database.EnsureCreated();

    var johnsBlog = new Blog("My Blog");
    var jacksBlog = new Blog("By Jack");
    ctx.Authors.AddRange(new Author[]
    {
        new("John Doe")
        {
            Posts =
            {
                new Post("First", "Hahaha", DateTime.UtcNow)
                {
                    Blog = johnsBlog
                },
                new Post("Second", "Hahaha2", DateTime.UtcNow)
                {
                    Blog = johnsBlog
                }
            }
        },
        new("Jack Ritcher")
        {
            Posts =
            {
                new Post("First", "Hahaha", DateTime.UtcNow)
                {
                    Blog = jacksBlog

                }
            }
        },
    });

    Console.Clear();
    ctx.SaveChanges();
    Console.Clear();
}

using (var scope = serviceProvider.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<WhatsNewContext>();
    // var count = ctx.Posts.Count();
    // ctx.Posts
    //     .Where(p => p.Author.Name.StartsWith("John"))
    //     .ExecuteDelete();
    // var count = ctx.Posts.Count();
    
    // ctx.Posts
    //     .Where(p => p.Author.Name.StartsWith("John") && p.Title == "First")
    //     .ExecuteUpdate(p => p.SetProperty(p => p.Title, "First2"));
    ctx.Posts
        .Where(p => p.Author.Name.StartsWith("John") && p.Title == "First")
        .ExecuteUpdate(p => p.SetProperty(p => p.Title, p => p.Title + " Post"));
    var posts = ctx.Posts.ToList();
}