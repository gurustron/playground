using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// builder.Services.AddAuthentication()
//     .AddCookie(options =>
//     {
//         options.Cookie.Expiration
//         options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
//     });
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".AdventureWorks.Session";
    options.Cookie.IsEssential = true;
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession()
     .Use( async (context, func) =>
     {
         var int32 = context.Session.GetInt32("test");
         if (int32 is null)
         {
             context.Session.SetInt32("test", 1);
         }

         context.Response.Cookies.Append("test", "test", new CookieOptions
        {
            Path = "/"
        });
        await func();
        var responseCookies = context.Response.Cookies;
    })
    ;
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");




app.Run();