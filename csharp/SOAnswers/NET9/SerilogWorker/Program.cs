using Serilog;
using SerilogWorker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddSerilog((services, loggerConfiguration) => loggerConfiguration
    .ReadFrom.Configuration(builder.Configuration));
var host = builder.Build();
host.Run();

//  "Logging": {
//    "LogLevel": {
//      "Default": "Information",
//      "Microsoft.Hosting.Lifetime": "Information"
//    }
//  }