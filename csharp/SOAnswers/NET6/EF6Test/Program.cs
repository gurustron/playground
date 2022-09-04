// See https://aka.ms/new-console-template for more information

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Hello, World!");
var serviceCollection = new ServiceCollection();
serviceCollection.AddDbContext<SomeContext>(builder => builder.UseSqlite($"Filename={nameof(SomeContext)}.db"));
var serviceProvider = serviceCollection.BuildServiceProvider();

using (var scope = serviceProvider.CreateScope())
{
    var ctx = scope.ServiceProvider.GetService<SomeContext>();
    ctx.Database.EnsureDeleted();
    ctx.Database.EnsureCreated();
    ctx.Images.Add(new Image
    {
        Path = "qwerty",
    });
    ctx.Messages.Add(new Message
    {
        MessageText = "qwerty",
        Images = new List<Image>
        {
            new Image
            {
                Path = "qwerty",
            },
        }
    });
    ctx.SaveChanges();
}

using (var scope = serviceProvider.CreateScope())
{
    var ctx = scope.ServiceProvider.GetService<SomeContext>();
    var message = ctx.Messages.First();
    var listAsync = await ctx.Images
        .Where(i => !i.Messages.Any() || i.Messages.Any(m => m.MessageId == message.MessageId))
        .ToListAsync();
}

public class SomeContext : DbContext
{
    public SomeContext(DbContextOptions<SomeContext> options) : base(options)
    {
    }

    public DbSet<Message> Messages { get; set; }
    public DbSet<Image> Images { get; set; }
}

public class Message
{
    public int MessageId { get; set; }
    public string MessageText { get; set; }
    public ICollection<Image> Images { get; set; }
}

public class Image
{
    public int ImageId {get;set;}

    [Required, StringLength(2048)]
    public string Path {get;set;}

    public virtual ICollection<Message> Messages { get; set; }
}