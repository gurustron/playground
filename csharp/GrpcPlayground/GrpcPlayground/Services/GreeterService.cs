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
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                IsDescending = request.IsDescending
            });
        }
    }

    public override async Task<ExampleResponse> StreamingFromClient(IAsyncStreamReader<ExampleRequest> requestStream, ServerCallContext context)
    {
        var size = 0;
        var index = 0;

        await foreach (var curr in requestStream.ReadAllAsync())
        {
            size += curr.PageSize;
            index += curr.PageIndex;
        }

        return new()
        {
            PageIndex = index,
            PageSize = size
        };
    }

    public override async Task StreamingBothWays(IAsyncStreamReader<ExampleRequest> requestStream, IServerStreamWriter<ExampleResponse> responseStream, ServerCallContext context)
    {
        await foreach (var curr in requestStream.ReadAllAsync())
        {
            await responseStream.WriteAsync(new ExampleResponse
            {
                PageIndex = curr.PageIndex,
                PageSize = curr.PageSize,
                IsDescending = curr.IsDescending
            });
        }
    }
}