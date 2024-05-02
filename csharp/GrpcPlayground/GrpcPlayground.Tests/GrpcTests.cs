using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc.Testing;

namespace GrpcPlayground.Tests;

public class GrpcTests
{
    private WebApplicationFactory<Program> _factory;

    [OneTimeSetUp]
    public void Setup() => _factory = new WebApplicationFactory<Program>();

    [Test]
    public async Task SayHello()
    {
        using var httpClient = _factory.CreateClient();

        var channel = GrpcChannel.ForAddress(httpClient.BaseAddress, new GrpcChannelOptions
        {
            HttpClient = httpClient
        });

        var greeterClient = new Greeter.GreeterClient(channel);

        const string name = "MyNameIsSlimShady";

        var helloReply = await greeterClient.SayHelloAsync(new HelloRequest {Name = name});
        
        Assert.That(helloReply.Message, Is.EqualTo($"Hello {name}"));
        Assert.That(helloReply.Start, Is.Null);
        Assert.That(helloReply.Duration, Is.Null);
        Assert.That(helloReply.Age, Is.Null);
        Assert.That(helloReply.Roles, Is.Empty);
    }    
    
    [Test]
    public async Task StreamingFromServer()
    {
        using var httpClient = _factory.CreateClient();

        var channel = GrpcChannel.ForAddress(httpClient.BaseAddress, new GrpcChannelOptions
        {
            HttpClient = httpClient
        });

        var greeterClient = new Greeter.GreeterClient(channel);

        var request = new ExampleRequest
        {
            PageIndex = 42,
            PageSize = 3
        };

        using var asyncServerStreamingCall = greeterClient.StreamingFromServer(request);

        var responses = await asyncServerStreamingCall.ResponseStream.ReadAllAsync().ToArrayAsync();
        
        Assert.That(responses, Has.Exactly(request.PageSize).Items);

        Assert.That(responses, Has.Exactly(request.PageSize).Items.Property(nameof(ExampleResponse.PageIndex)).EqualTo(request.PageIndex));
    }
    
    [Test]
    public async Task StreamingFromClient()
    {
        using var httpClient = _factory.CreateClient();

        var channel = GrpcChannel.ForAddress(httpClient.BaseAddress, new GrpcChannelOptions
        {
            HttpClient = httpClient
        });

        ExampleRequest[] requests = [new() { PageSize = 1, PageIndex = 1}, new() { PageSize = 41, PageIndex = 7}];

        var greeterClient = new Greeter.GreeterClient(channel);
        
        using var asyncClientStreamingCall = greeterClient.StreamingFromClient(new CallOptions(new Metadata {{"key1", "value1"}}));
        foreach (var request in requests)
        {
            await asyncClientStreamingCall.RequestStream.WriteAsync(request);
        }
        await asyncClientStreamingCall.RequestStream.CompleteAsync();

        var exampleResponse = await asyncClientStreamingCall.ResponseAsync;
        
        Assert.That(exampleResponse.PageSize, Is.EqualTo(requests.Sum(r => r.PageSize)));
        Assert.That(exampleResponse.PageIndex, Is.EqualTo(requests.Sum(r => r.PageIndex)));
    } 
    
    [Test]
    public async Task StreamingBothWays()
    {
        using var httpClient = _factory.CreateClient();

        var channel = GrpcChannel.ForAddress(httpClient.BaseAddress, new GrpcChannelOptions
        {
            HttpClient = httpClient
        });

        ExampleRequest[] requests = [new() { PageSize = 1, PageIndex = 1}, new() { PageSize = 41, PageIndex = 7}];

        var greeterClient = new Greeter.GreeterClient(channel);
        
        using var asyncClientStreamingCall = greeterClient.StreamingBothWays(new CallOptions(new Metadata {{"key1", "value1"}}));
        foreach (var request in requests)
        {
            await asyncClientStreamingCall.RequestStream.WriteAsync(request);
        }
        await asyncClientStreamingCall.RequestStream.CompleteAsync();

        var responses = await asyncClientStreamingCall.ResponseStream.ReadAllAsync().ToArrayAsync();
        
        Assert.That(responses, Has.Length.EqualTo(requests.Length));

        foreach (var (expected, actual) in requests.Zip(responses))
        {
            Assert.That(actual.PageIndex, Is.EqualTo(expected.PageIndex));
            Assert.That(actual.PageSize, Is.EqualTo(expected.PageSize));
            Assert.That(actual.IsDescending, Is.EqualTo(expected.IsDescending));
        }
    } 
}

