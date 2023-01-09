using System.Net;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);
var s = builder.Configuration["MY:secret"];
var environmentVariable = Environment.GetEnvironmentVariable("MY__SECRET");
// Add services to the container.
builder.Services.AddControllersWithViews();

List<string> values = new List<string?> { null, "", "value" }
    .Where(x => !string.IsNullOrEmpty(x))
    .ToList();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
