using System.Transactions;
using EF7WhatsNew.Db.Animals;
using EF7WhatsNew.Db.Blogginng;
using EF7WhatsNew.Misc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using IsolationLevel = System.Transactions.IsolationLevel;

// InterceptorTests.Do();
// await TPCAnimalsRunner.Do();

Console.WriteLine("Hello, World!");

var serviceCollection = new ServiceCollection();
serviceCollection.AddDbContext<WhatsNewContext>(builder =>
    builder.UseNpgsql("Host=localhost;Port=6432;Database=whatsnewef_db;Username=postgres;Password=P@ssword")
        .LogTo(Console.WriteLine, LogLevel.Information));
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
                    Blog = johnsBlog, Tags = { new Tag("data", "data"),new Tag("newcomer", "new author") }
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
    var posts = ctx.Posts.Include(p => p.Tags).ToList();
    var authorsWithPosts = ctx.Posts
        .GroupBy(post => post.Author)
        .Select(grouping => new { Author = grouping.Key, Posts = string.Join("|", grouping.Select(post => post.Title)) })
        .ToList();

    // var timeZoneHandling = ctx.Posts
    //     .Select(
    //         post => new
    //         {
    //             post.Title,
    //             PacificTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(post.PublishedOn, "America/Denver"),
    //             UkTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(post.PublishedOn, "GMT"),
    //         }).ToList();
    //
    // var query = ctx.Posts
    //     .GroupBy(p => p.AuthorId);
    // await foreach (var group in query.AsAsyncEnumerable())
    // {
    //     Console.WriteLine($"Price: {group.Key}; Count = {group.Count()}");
    // }
    //5
    // var query1 = ctx.Posts.GroupJoin(
    //     ctx.Authors, c => c.AuthorId, o => o.Id, (c, os) => new { Blog = c, Authors = os });
    //
    // await foreach (var t in query1.AsAsyncEnumerable())
    // {
    //     Console.WriteLine($"Title: {t.Blog.Title};  = {t.Authors.Count()}");
    // }
    
    var post = ctx.Posts.Find(1);


    post!.AuthorId = 2;
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
    var first = ctx.Posts.First();
    first.Title += "Updated";
    first.Content += "Updated";
    first.PublishedOn = DateTime.UtcNow.AddDays(1);
    ctx.SaveChanges();
}

public static class BlogsContextExtensions
{
    #region FindSiblings
    public static IEnumerable<TEntity> FindSiblings<TEntity>(
        this DbContext context, TEntity entity, string navigationToParent)
        where TEntity : class
    {
        var parentEntry = context.Entry(entity).Reference(navigationToParent);

        return context.Entry(parentEntry.CurrentValue!)
            .Collection(parentEntry.Metadata.Inverse!)
            .CurrentValue!
            .OfType<TEntity>()
            .Where(e => !ReferenceEquals(e, entity));
    }
    #endregion
}



