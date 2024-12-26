using System.Text;
using System.Text.Unicode;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Internal;
using Microsoft.Extensions.FileProviders.Physical;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

var builder = WebApplication.CreateBuilder(args);

int Do(int one = 1, int two = 2, int three = 3) => three;
Console.WriteLine(Do(three: 4));
// Add services to the container.
builder.Services.AddControllers().AddControllersAsServices();
// builder.Services.AddProblemDetails();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddTransient<IActionResultExecutor<ObjectResult>, MyObjectResultExecutor>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// app.UseHttpsRedirection();
var myFileProvider = new MyFileProvider(
    Path.Combine(Directory.GetCurrentDirectory(), @"MyStaticFiles"));
app.UseFileServer(new FileServerOptions()
{
    FileProvider = myFileProvider,
    RequestPath = new PathString("/StaticFiles"),
    EnableDirectoryBrowsing = true,
    // StaticFileOptions = { FileProvider = myFileProvider}
});
app.UseAuthorization();

app.MapControllers();

app.Run();

class MyFileProvider : IFileProvider
{
    private readonly string _root;
    private readonly PhysicalFileProvider _provider;

    public MyFileProvider(string root)
    {
        _root = root;
        _provider = new PhysicalFileProvider(_root);
    }
    public IDirectoryContents GetDirectoryContents(string subpath) => _provider.GetDirectoryContents(subpath);

    public IFileInfo GetFileInfo(string subpath)
    {
        var fileInfo = _provider.GetFileInfo(subpath);
        return new MyFileInfo(fileInfo);
    }

    public IChangeToken Watch(string filter)
    {
        throw new NotImplementedException();
    }
}

class MyFileInfo : IFileInfo
{
    private static readonly ReadOnlyMemory<byte> ToAppend = new(" Hello World!"u8.ToArray());
    private readonly IFileInfo _fileInfo;

    public MyFileInfo(IFileInfo fileInfo) => _fileInfo = fileInfo;

    public Stream CreateReadStream()
    {
        var readStream = _fileInfo.CreateReadStream();
        var memoryStream = new MemoryStream();
        readStream.CopyTo(memoryStream);
        memoryStream.Write(ToAppend.Span);
        memoryStream.Seek(0, SeekOrigin.Begin);
        return memoryStream;
    }
    public DateTimeOffset LastModified  => DateTimeOffset.UtcNow;
    public long Length  => _fileInfo.Length + ToAppend.Length;
    public string? PhysicalPath  => null;
    
    public bool Exists => _fileInfo.Exists;
    public bool IsDirectory => _fileInfo.IsDirectory;
    public string Name => _fileInfo.Name;
}

class MyObjectResultExecutor : ObjectResultExecutor
{
    private readonly ProblemDetailsFactory _problemDetailsFactory;

    public MyObjectResultExecutor(
        ProblemDetailsFactory problemDetailsFactory,
        OutputFormatterSelector formatterSelector,
        IHttpResponseStreamWriterFactory writerFactory,
        ILoggerFactory loggerFactory,
        IOptions<MvcOptions> mvcOptions) : base(formatterSelector, writerFactory, loggerFactory, mvcOptions)
    {
        _problemDetailsFactory = problemDetailsFactory;
    }

    public override Task ExecuteAsync(ActionContext context, ObjectResult result)
    {
        if (result is BadRequestObjectResult badRequestResult)
        {
            var actual = _problemDetailsFactory.CreateProblemDetails(context.HttpContext,
                400);
            actual.Extensions.Add("error", badRequestResult.Value);
            
            return base.ExecuteAsync(context, new ObjectResult(actual)
            {
                StatusCode = actual.Status,
            });
        }
        return base.ExecuteAsync(context, result);
    }
}