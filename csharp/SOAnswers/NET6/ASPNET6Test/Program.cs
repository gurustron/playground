using AutoMapper;
using NET6LibTest;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddSingleton<SomeLibraryClass>();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfiles(new[] { new MappingProfile() });
}); 

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public class Test
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int I { get; set; }
}

class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Test, Test>();
    }
}
