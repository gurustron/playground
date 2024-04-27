using Grpc.Net.Client;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace GrpcPlayground.Tests;

public class GrpcTests
{
    private WebApplicationFactory<Program> _factory;

    [OneTimeSetUp]
    public void Setup() => _factory = new WebApplicationFactory<Program>();

    [Test]
    public async Task Do()
    {
        using var httpClient = _factory.CreateClient();
        var stringAsync = await httpClient.GetStringAsync("/");
        var channel = GrpcChannel.ForAddress(httpClient.BaseAddress, new GrpcChannelOptions
        {
            HttpClient = httpClient
        });
        

        var greeterClient = new Greeter.GreeterClient(channel);

        var helloReply = await greeterClient.SayHelloAsync(new HelloRequest(){Name = "qweqeqw"});
        Console.WriteLine(helloReply.Message);
        Console.WriteLine(helloReply.Duration);
        Console.WriteLine(helloReply.Message);
    }
}

