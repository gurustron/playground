// See https://aka.ms/new-console-template for more information

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

Console.WriteLine("Hello, World!");

var serviceCollection = new ServiceCollection();
serviceCollection.AddDbContext<WhatsNewContext>(builder =>
    builder.UseSqlite("Data Source=test.db").UseSqlServer("as")
        .LogTo(Console.WriteLine, LogLevel.Information));
var serviceProvider = serviceCollection.BuildServiceProvider();

using (var scope = serviceProvider.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<WhatsNewContext>();
    ctx.Database.EnsureDeleted();
    ctx.Database.EnsureCreated();
}

public class WhatsNewContext : DbContext
{
    public WhatsNewContext(DbContextOptions<WhatsNewContext> opts) : base(opts)
    {
    }

    public DbSet<Author> Authors { get; set; }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
}

public class Author
{
    public int Id { get; set; }
}