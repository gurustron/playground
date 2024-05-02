using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

namespace GrpcPlayground.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
    
    
    [Test]
    public void RuntimeHelpersGetHashCode_Null_HashCodeIsZero()
    {
        Assert.AreEqual(0, RuntimeHelpers.GetHashCode(null));
    }
    
    [Test]
    public void FactoryTypes()
    {
var services = new ServiceCollection();
services.AddTransient<ITransportFactory, TransportFactory>();
services.AddTransient<ISomeDepFromDi, SomeDepFromDi>();
var serviceProvider = services.BuildServiceProvider();
var transportFactory = serviceProvider.GetRequiredService<ITransportFactory>();

Assert.IsInstanceOf<Car>(transportFactory.GetTransport(new CarConfig()));
Assert.IsInstanceOf<Boat>(transportFactory.GetTransport(new BoatConfig()));
    }
}

public interface ITransport
{
    string Type { get; }
    void Move();
}
public class CarConfig : IConfig;
public class Car(CarConfig config) : ITransport
{
    public string Type => nameof(Car);
    public void Move() { /* logic */ }
}
public class BoatConfig : IConfig;
public class Boat(BoatConfig config, ISomeDepFromDi dep) : ITransport
{
    public string Type => nameof(Boat);
    public void Move() { /* logic */ }
}

public interface ISomeDepFromDi;

public class SomeDepFromDi : ISomeDepFromDi;

public interface ITransportFactory
{
    ITransport GetTransport<T>(T config) where T : IConfig;
}

public class TransportFactory(IServiceProvider serviceProvider) : ITransportFactory
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    private static Dictionary<Type, Type> Map = new()
    {
        { typeof(BoatConfig), typeof(Boat) },
        { typeof(CarConfig), typeof(Car) },
    };

    public ITransport GetTransport<T>(T config) where T : IConfig
    {
        if (Map.TryGetValue(typeof(T), out var transportType))
        {
            return ActivatorUtilities.CreateInstance(serviceProvider, transportType, config) as ITransport;
        }

        throw new InvalidOperationException($"Unknown config type {typeof(T)}");
    }
}

public interface IConfig;