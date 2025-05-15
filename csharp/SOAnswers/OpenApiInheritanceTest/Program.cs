using System.Text.Json.Serialization;

namespace OpenApiInheritanceTest;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        // app.UseHttpsRedirection();

        // app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
public class RequestVehicle
{
    public Vehicle Vehicle { get; set; }
}

[JsonDerivedType(typeof(Car), "car")]
public abstract class Vehicle : IThingService
{
    public string Status { get; set; }
    public string Name { get; set; }
    public abstract void Start();

    protected Vehicle()
    {
        Status = "Not running";
    }




    async Task<T?> IThingService.GetThing<T>() where T:default
    {

// Try and get the thing from the DB
        var result = await Task.FromResult(Activator.CreateInstance<T>());
        if (result is null) return default;

        return result; // Throws error
    }
}

public interface IThingService {
Task<T?> GetThing<T>();
}

public class Car : Vehicle
{
    public Car()
    {
        Name = "Car";
    }

    public override void Start()
    {
        Console.WriteLine("Broemmm pruttle put");
        Status = "Running";
    }
}
