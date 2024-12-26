var builder = WebApplication.CreateBuilder();

var queryConfigList = builder.Configuration.GetSection("QueryConfigList").Get<List<QueryConfig>>();
foreach (var entry in builder.Configuration.GetSection("QueryConfigList").GetChildren())
{
    foreach (var field in entry.GetChildren())
    {
        if (field.Key == "ObjectClass")
        {
            // process dynamic
        }
    }
}
var type = queryConfigList[0].ObjectClass.GetType();
var val = queryConfigList[0].ObjectClass;
// var tagName = (string)val.TagName;
// Add services to the container.
builder.Services
    .AddApiVersioning(
        options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = false;
            options.ReportApiVersions = false;
        })
    .AddMvc()
    .AddApiExplorer(
        options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        } );builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=HomeApi}/{action=Index}/{id?}");

app.Run();

public class QueryConfig
{
    public string? TableName { get; set; }
    public string? QueryText { get; set; }
    public string? AdditionalInfo { get; set; }
    public Dictionary<string, string>? ObjectClass { get; set; }
}

public class QueryConfigList
{
    public ICollection<QueryConfig>? QueryConfig { get; set; }
}