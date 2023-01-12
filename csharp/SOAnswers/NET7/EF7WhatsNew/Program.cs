using System.Transactions;
using EF7WhatsNew.Db.Animals;
using EF7WhatsNew.Db.Blogginng;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using IsolationLevel = System.Transactions.IsolationLevel;

// await TPCAnimalsRunner.Do();
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
    
    await ctx.AddAsync(new Blog("MyNONTransaction_Blog"));
    await ctx.SaveChangesAsync();

    using (var tx = new TransactionScope(TransactionScopeOption.Required,
               new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
               TransactionScopeAsyncFlowOption.Enabled))
    {
        await ctx.AddAsync(new Blog("MyTransaction_Blog"));
        await ctx.SaveChangesAsync();
    }

    await using (var tx = await ctx.Database.BeginTransactionAsync())
    {
        await ctx.AddAsync(new Blog("MyTransaction_Blog"));

        await ctx.SaveChangesAsync();
    }

    Console.Clear();
}

using (var scope = serviceProvider.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<WhatsNewContext>();

    var post = ctx.Posts.Find(1);


    post.AuthorId = 2;
    ctx.SaveChanges();
    ctx.Authors.Find(2);
    // ctx.Authors.Find();
    // var count = ctx.Posts.Count();
    // ctx.Posts
    //     .Where(p => p.Author.Name.StartsWith("John"))
    //     .ExecuteDelete();
    // var count = ctx.Posts.Count();
    
    // ctx.Posts
    //     .Where(p => p.Author.Name.StartsWith("John") && p.Title == "First")
    //     .ExecuteUpdate(p => p.SetProperty(p => p.Title, "First2"));
    // ctx.Posts
    //     .Where(p => p.Author.Name.StartsWith("John") && p.Title == "First")
    //     .ExecuteUpdate(p => p.SetProperty(p => p.Title, p => p.Title + " Post"));
    var posts = ctx.Posts.ToList();
    var first = ctx.Posts.First();
    first.Content = first.Content + " Updated";
    first.CreatedOn = DateTime.UtcNow.AddDays(7);
    ctx.SaveChanges();
}
