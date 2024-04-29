using Grpc.Core;
using GrpcPlayground;

namespace GrpcPlayground.Services;

public class GreeterService : Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> _logger;

    public GreeterService(ILogger<GreeterService> logger)
    {
        _logger = logger;
    }

    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        return Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Name
        });
    }

    public override async Task StreamingFromServer(ExampleRequest request, IServerStreamWriter<ExampleResponse> responseStream, ServerCallContext context)
    {
        for (int i = 0; i < request.PageSize; i++)
        {
            await responseStream.WriteAsync(new ExampleResponse
            {
                PageIndex = request.PageIndex
            });
        }
    }

    public override async Task<ExampleResponse> StreamingFromClient(IAsyncStreamReader<ExampleRequest> requestStream, ServerCallContext context)
    {
        var result = 0;

        await foreach (var curr in requestStream.ReadAllAsync())
        {
            result += curr.PageSize;
        }

        return new()
        {
            PageIndex = result,
            PageSize = result
        };
    }
}