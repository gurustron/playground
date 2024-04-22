// See https://aka.ms/new-console-template for more information

using Grpc.Net.Client;
using GrpcPlayground;

Console.WriteLine("Hello, World!");

var channel = GrpcChannel.ForAddress("http://localhost:5054");

var greeterClient = new Greeter.GreeterClient(channel);

var helloReply =await greeterClient.SayHelloAsync(new HelloRequest(){Name = "qweqeqw"});
Console.WriteLine(helloReply.Message);