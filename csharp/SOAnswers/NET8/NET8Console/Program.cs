using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using ILogger = Microsoft.Extensions.Logging.ILogger;
// var span = GetMeSpan();
// var beforeMethodCall = string.Join(" ", span.ToArray());
// Console.WriteLine("Just to use the stack though previous uses it");
// var afterMethodCall = string.Join(" ", span.ToArray());
//
// Console.WriteLine(beforeMethodCall);
// Console.WriteLine(afterMethodCall);
// Host.CreateDefaultBuilder().UseDefaultServiceProvider(options => options.ValidateScopes = true).Build();
//
// Span<byte> GetMeSpan() {
//     var buffer = new WriteBuffer64();
//     var span = buffer.GetWriteSpan(42);
//     var beforeMethodCall = string.Join(" ", span.ToArray());
//      Console.WriteLine("Inner: Just to use the stack though previous uses it");
//     var afterMethodCall = string.Join(" ", span.ToArray());
//
//     Console.WriteLine(beforeMethodCall);
//     Console.WriteLine(afterMethodCall);
//     return span;
// }
//
// Environment.Exit(0);

var ints = new List<int>();
CollectionsMarshal.SetCount(ints, 20);
Console.WriteLine(ints.Count);
var serviceCollection = new ServiceCollection();

serviceCollection.AddTransient<ITest, Test>();
serviceCollection.AddTransient<ITest, Test2>();
serviceCollection.AddTransient(typeof(ObservableCollection<>));
serviceCollection.AddSingleton<MyClass>();
serviceCollection.AddLogging(builder => builder.AddConsole().AddSerilog().AddDebug());

var services = serviceCollection.BuildServiceProvider(new ServiceProviderOptions{ ValidateScopes = true, ValidateOnBuild = true });
var myClass = services.GetService<MyClass>();
var loggerProviders = services.GetServices<ILoggerProvider>();
Console.WriteLine(string.Join(", ", loggerProviders.Select(lp => lp.GetType().Name)));
var collection = ActivatorUtilities.CreateInstance<ObservableCollection<ITest>>(services);

Console.WriteLine("Hello, World!");

var ctx = new TestContext();
ctx.Database.EnsureDeleted();
ctx.Database.EnsureCreated();
ctx.Items.ToArray();
ctx.Items.Add(new() { Id = 1, Name = "as", Children = new(){
    new(){Identificator = 100, Name="asd"}
}});

var org = new Organization
{
    Name = "Test1"
};

ctx.Users.Add(new User()
{
    Following = new()
    {
        org,
        new Organization
        {
            Name = "Foo"
        }
    }
});

ctx.Users.Add(new User()
{
    Following = new()
    {
        org
    }
});
ctx.SaveChanges();
ctx.ChangeTracker.Clear();
Console.WriteLine(ctx.Items.First().Children.First().Identificator);
var users = ctx.Users.Include(user => user.Following).ToList();
Console.WriteLine();

public class SomeClass
{
    private readonly Person _person;

    public required Person Person
    {
        get => _person;
        [MemberNotNull(nameof(_person))]
        init
        {
            ArgumentNullException.ThrowIfNull(value);
            _person = value;
        }
    }

}

public class Person
{
    void Main1() {
        var span = GetMeSpan();
        var beforeMethodCall = string.Join(" ", span.ToArray());
        // Console.WriteLine("Just to use the stack");
        var afterMethodCall = string.Join(" ", span.ToArray());

        Console.WriteLine(beforeMethodCall);
        Console.WriteLine(afterMethodCall);
    }

    Span<byte> GetMeSpan() {
        var buffer = new WriteBuffer64();
        return buffer.GetWriteSpan(42);
    }
}

// https://stackoverflow.com/questions/79236969/is-it-safe-to-take-the-reference-of-an-inline-fixed-size-buffer-of-a-struct-to-u/79237926#79237926
internal struct WriteBuffer64
{
    private const int INLINE_BUFFER_SIZE = 64;
    private unsafe fixed byte buffer[INLINE_BUFFER_SIZE];
    private int used;

    public unsafe Span<byte> GetWriteSpan(int toWrite)
    {
        if (used + toWrite > INLINE_BUFFER_SIZE)
            throw new Exception();

        int offset = used;
        used += toWrite;
        return MemoryMarshal.CreateSpan(ref buffer[offset], toWrite);
    }

    // ...
}
class TestContext : DbContext
{
    public DbSet<TestItem> Items { get; set; }
    public DbSet<Organization> Organizations => Set<Organization>();
    public DbSet<User> Users => Set<User>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=test.db");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.Entity<TestItem>().OwnsMany(ti => ti.Children, builder => builder.ToJson());
    }
}

public class Organization {
    public int Id { get; set; }

    public string Name { get; set; }
    // lots of properties
// NO List<User> Followers { get; set; }
}

public class User {
    public int Id { get; set; }
// lots of properties
    public List<Organization> Following { get; set; }
}

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasMany(u => u.Following)
            .WithMany();
    }
}
class TestItem
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required List<TestChild> Children { get; set; }
}

class TestChild
{
    public required int Identificator { get; set; }
    public required string Name { get; set; }
}

public interface ITest { }

public class Test : ITest { }

public class Test2 : ITest { }

internal readonly struct MessageLogger
{
    public MessageLogger(ILogger logger, string category, string? providerTypeFullName, LogLevel? minLevel, Func<string?, string?, LogLevel, bool>? filter)
    {
        Logger = logger;
        Category = category;
        ProviderTypeFullName = providerTypeFullName;
        MinLevel = minLevel;
        Filter = filter;
    }
 
    public ILogger Logger { get; }
 
    public string Category { get; }
 
    private string? ProviderTypeFullName { get; }
 
    public LogLevel? MinLevel { get; }
 
    public Func<string?, string?, LogLevel, bool>? Filter { get; }
 
    public bool IsEnabled(LogLevel level)
    {
        if (MinLevel != null && level < MinLevel)
        {
            return false;
        }
 
        if (Filter != null)
        {
            return Filter(ProviderTypeFullName, Category, level);
        }
 
        return true;
    }
}

record MyClass(ITest test);