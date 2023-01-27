using System.Threading.RateLimiting;
using ASP7WhatsNew;
using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

#region options

builder.Services.Configure<MyRateLimitOptions>(
    builder.Configuration.GetSection(MyRateLimitOptions.MyRateLimit));

var myOptions = new MyRateLimitOptions();
builder.Configuration.GetSection(MyRateLimitOptions.MyRateLimit).Bind(myOptions);

#endregion

var fixedPolicy = "fixed";
var slidingPolicy = "sliding";
builder.Services.AddRateLimiter(_ => _
    .AddFixedWindowLimiter(policyName: fixedPolicy, options =>
    {
        options.PermitLimit = myOptions.PermitLimit;
        options.Window = TimeSpan.FromSeconds(myOptions.Window);
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = myOptions.QueueLimit;
    })
    .AddSlidingWindowLimiter(policyName: slidingPolicy, options =>
    {
        options.PermitLimit = myOptions.PermitLimit;
        options.Window = TimeSpan.FromSeconds(myOptions.Window);
        options.SegmentsPerWindow = myOptions.SegmentsPerWindow;
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = myOptions.QueueLimit;
    }));

var selectedPolicy = slidingPolicy;

var app = builder.Build();
app.UseRateLimiter();
app.MapGet("/", () => Results.Ok($"Hello with policy - {selectedPolicy}: {GetTicks()}"))
    .RequireRateLimiting(selectedPolicy);

app.Run();

static string GetTicks() => (DateTime.Now.Ticks & 0x11111).ToString("00000");