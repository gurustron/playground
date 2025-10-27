using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLocalization(opts => opts.ResourcesPath = "Resources");

builder.Services.AddControllers()
    .AddDataAnnotationsLocalization();

builder.Services.AddRazorPages()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();

builder.Services.Configure<RequestLocalizationOptions>(opts =>
{
    var supported = new[] { "sr-Latn-ME", "en", "ru", "tr" }
        .Select(c => new CultureInfo(c)).ToList();

    opts.DefaultRequestCulture = new RequestCulture("sr-Latn-ME");
    opts.SupportedCultures = supported;
    opts.SupportedUICultures = supported;
    opts.RequestCultureProviders = new[]
    {
        new CookieRequestCultureProvider()
    };
});

var app = builder.Build();

var locOptions = app.Services
    .GetRequiredService<IOptions<RequestLocalizationOptions>>()
    .Value;

app.UseRequestLocalization(locOptions);

app.MapControllers();
app.MapRazorPages();
app.Run();



// var builder = WebApplication.CreateBuilder(args);
//
// // Add services to the container.
// builder.Services.AddRazorPages();
//
// var app = builder.Build();
//
// // Configure the HTTP request pipeline.
// if (!app.Environment.IsDevelopment())
// {
//     app.UseExceptionHandler("/Error");
//     // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//     app.UseHsts();
// }
//
// app.UseHttpsRedirection();
//
// app.UseRouting();
//
// app.UseAuthorization();
//
// app.MapStaticAssets();
// app.MapRazorPages()
//     .WithStaticAssets();
//
// app.Run();