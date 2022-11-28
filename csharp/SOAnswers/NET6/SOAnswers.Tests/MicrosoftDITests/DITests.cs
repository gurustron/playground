using Microsoft.Extensions.DependencyInjection;

namespace SOAnswers.Tests.MicrosoftDITests;

public class DITests
{
    [Test]
    public void Do()
    {
        IServiceCollection services = new ServiceCollection();
        services.AddScoped<Dep>();
        services.AddScoped<IDep, Dep>();
        var serviceProvider = services.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        scope.ServiceProvider.GetService<int>();
        scope.ServiceProvider.GetRequiredService<IDep>();
    }

    interface IDep
    {
        
    }

    class Dep:IDep
    {
        public Dep()
        {
            Console.WriteLine("Dep");
        }
    }
}