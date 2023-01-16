using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace EF7WhatsNew.Misc;

public class InterceptorTests
{
    public static void Do()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddTransient<IValueProvider, ValueProvider>();
        serviceCollection.AddDbContext<SomeContext>(builder => 
            builder.UseSqlite($"Filename={nameof(SomeContext)}.db")
                .AddInterceptors(NotMappedValueGeneratingInterceptor.Instance));
        var serviceProvider = serviceCollection.BuildServiceProvider();
// init db and add one item
        using (var scope = serviceProvider.CreateScope())
        {
            var someContext = scope.ServiceProvider.GetRequiredService<SomeContext>();
            someContext.Database.EnsureDeleted();
            someContext.Database.EnsureCreated();
            someContext.Add(new MyEntity());
            someContext.SaveChanges();
        }

// check that value provider is used
        using (var scope = serviceProvider.CreateScope())
        {
            var someContext = scope.ServiceProvider.GetRequiredService<SomeContext>();
            var myEntities = someContext.Entities.ToList();
            Console.WriteLine(myEntities.First().NotMapped); // prints "From DI"
        }
    }
}

public class MyEntity
{
    public MyEntity()
    {        
    }

    // public MyEntity(SomeContext context)
    // {
    //     var valueProvider = context.GetService<IValueProvider>();
    //     NotMapped = valueProvider.GetValue();
    // }

    public int Id { get; set; }

    [NotMapped]
    public string NotMapped { get; set; }
}


// Example value provider:
public interface IValueProvider
{
    string GetValue();
}

class ValueProvider : IValueProvider
{
    public string GetValue() => "From DI";
}

public class SomeContext : DbContext
{
    public SomeContext(DbContextOptions<SomeContext> options) : base(options)
    {
    }

    public DbSet<MyEntity> Entities => Set<MyEntity>();
}

class NotMappedValueGeneratingInterceptor : IMaterializationInterceptor
{
    public static NotMappedValueGeneratingInterceptor Instance = new ();
    public object InitializedInstance(MaterializationInterceptionData materializationData, object entity)
    {
        if (entity is MyEntity my)
        {
            var valueProvider = materializationData.Context.GetService<IValueProvider>();
            my.NotMapped = valueProvider.GetValue();
        }
        
        return entity;
    }
}