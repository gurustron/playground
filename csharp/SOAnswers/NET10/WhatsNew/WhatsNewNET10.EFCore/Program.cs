// See https://aka.ms/new-console-template for more information

using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Text.Json;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Npgsql;

Console.WriteLine("Hello, World!");
SqlMapper.AddTypeHandler(typeof(List<UrlContainer>), new CustomerJsonObjectTypeHandler());
NpgsqlConnection.GlobalTypeMapper.EnableDynamicJson();
var serviceCollection = new ServiceCollection();
var connectionString = "Host=localhost;Port=6432;Database=test_db;Username=postgres;Password=P@ssword";
serviceCollection.AddDbContext<TestDbContext>(builder => builder.UseNpgsql(connectionString)
    .LogTo(Console.WriteLine, LogLevel.Information));
var sp = serviceCollection.BuildServiceProvider();
var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
dataSourceBuilder.EnableDynamicJson();  
await using var dataSource = dataSourceBuilder.Build();

var participants = new Barrier(2);

async Task Act(int i)
{
    await Task.Delay(10);
    await using (var npgsqlConnection = dataSource.CreateConnection())
    {
        npgsqlConnection.Open();
        participants.SignalAndWait();
        var someEntities = npgsqlConnection.QuerySingle<SomeEntity>(
                """
                update public.media 
                SET thumbnails = thumbnails || @Thumbnails::jsonb
                WHERE id = @Id
                RETURNING *
                """,
                new SomeEntity { Thumbnails = [new UrlContainer { Url = $"Form {i}" }], Id = 1 });

        Console.WriteLine(JsonSerializer.Serialize(someEntities.Thumbnails));
    }
}

var t1 = Act(1);
var t2 = Act(2);
await Task.WhenAll(t1, t2);
// using (var scope = sp.CreateScope())
// {
//     var dbConnection = scope.ServiceProvider.GetRequiredService<TestDbContext>().Database.GetDbConnection();
//     dbConnection.Open();
//    
// }


public class TestDbContext : DbContext
{
    public TestDbContext(DbContextOptions<TestDbContext> opts) : base(opts)
    {
    }
}

public class SomeEntity
{
    public int Id { get; set; }
    [Column(TypeName = "jsonb")]
    public List<UrlContainer> Thumbnails { get; set; }
}

public class UrlContainer
{
    public string Url { get; set; }
}

public class CustomerJsonObjectTypeHandler : SqlMapper.TypeHandler<List<UrlContainer>>
{
    public override void SetValue(IDbDataParameter parameter, List<UrlContainer>? value)
    {
        parameter.Value = (value == null)
            ? (object)DBNull.Value
            : JsonSerializer.Serialize(value);
        parameter.DbType = DbType.String;
    }

    public override List<UrlContainer> Parse(object value)
    {
        var json = value.ToString();
        return json is null? null :JsonSerializer.Deserialize<List<UrlContainer>>(json);
    }
}