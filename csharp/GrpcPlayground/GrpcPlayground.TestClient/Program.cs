using Grpc.Core;
using Grpc.Net.Client;
using GrpcPlayground;

Console.WriteLine("Hello, World!");

var channel = GrpcChannel.ForAddress("http://localhost:5054");

var greeterClient = new Greeter.GreeterClient(channel);

var helloReply = await greeterClient.SayHelloAsync(new HelloRequest(){Name = "qweqeqw"});
Console.WriteLine(helloReply.Message);
Console.WriteLine(helloReply.Duration);

using var asyncServerStreamingCall = greeterClient.StreamingFromServer(new ExampleRequest
{
    PageIndex = 42,
    PageSize = 3
});
Console.WriteLine("-----------");
Console.WriteLine("Resp Streaming");
await foreach (var curr in asyncServerStreamingCall.ResponseStream.ReadAllAsync())
{
    Console.WriteLine(curr.PageIndex);
}
Console.WriteLine("-----------");
Console.WriteLine("Req Streaming");

using var asyncClientStreamingCall = greeterClient.StreamingFromClient(new CallOptions(new Metadata {{"key1", "value1"}}));
await asyncClientStreamingCall.RequestStream.WriteAsync(new ExampleRequest{ PageSize = 1});
await asyncClientStreamingCall.RequestStream.WriteAsync(new ExampleRequest{ PageSize = 41});
await asyncClientStreamingCall.RequestStream.CompleteAsync();

var exampleResponse = await asyncClientStreamingCall.ResponseAsync;
Console.WriteLine($"{exampleResponse.PageIndex} : {exampleResponse.PageSize}");

