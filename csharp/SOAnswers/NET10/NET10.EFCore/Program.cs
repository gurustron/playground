// See https://aka.ms/new-console-template for more information

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Npgsql;

Console.WriteLine("Hello, World!");

NpgsqlConnection.GlobalTypeMapper.EnableDynamicJson(new[] { typeof(CustomerDetails) });
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
    ctx.Customers.Add(new Customer
    {
        Details = new CustomerDetails
        {
            Age = 1,
            Name = "John",
            Orders = [new Order { Price = 100, ShippingAddress = "Somewhere" }]
        }
    });
    ctx.SaveChanges();

    var list = ctx.Customers
        .Select(c => WhatsNewContext.JsonExtract(, "age"))
        .ToList();
}

public class WhatsNewContext : DbContext
{
    public WhatsNewContext(DbContextOptions<WhatsNewContext> opts) : base(opts)
    {
    }
    
    [DbFunction("jsonb_extract_path_text", IsBuiltIn = true)]
    public static string JsonExtract(string json, string key)
        => throw new NotSupportedException("This method is for use with LINQ only.");
    
    public DbSet<Customer> Customers => Set<Customer>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDbFunction(
            typeof(WhatsNewContext).GetMethod(nameof(WhatsNewContext.JsonExtract))!
        );
        
        modelBuilder.Entity<Customer>()
            .ComplexProperty(c => c.Details, d => d.ToJson());
    }
}

public class Customer
{
    public int Id { get; set; }
    public CustomerDetails Details { get; set; }
}

public class CustomerDetails // Map to a JSON column in the table
{
    public string Name { get; set; }
    public int Age { get; set; }
    public List<Order> Orders { get; set; }
}

public class Order // Part of the JSON column
{
    public decimal Price { get; set; }
    public string ShippingAddress { get; set; }
}