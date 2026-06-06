using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Npgsql;
using Npgsql.NameTranslation;

Console.WriteLine("Hello, World!");
//     
// var dataSourceBuilder = new NpgsqlDataSourceBuilder(
//     "Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=mysecretpassword;");
// dataSourceBuilder.MapComposite<PgDateTimeOffset>("datetimeoffset", new NpgsqlNullNameTranslator());
// var dataSource = dataSourceBuilder.Build();
// SqlMapper.AddTypeMap(typeof(PgDateTimeOffset), DbType.Object);
//
// using var conn = dataSource.CreateConnection();
// conn.Open();
//
// PgDateTimeOffset timestamp = new PgDateTimeOffset
// {
//     DateTimeUtc = DateTime.Now,
//     Offset = 3
// };
// conn.Execute("INSERT INTO mytable (Timestamp) VALUES (@timestamp)", new { timestamp });
// var pgDateTimeOffsets = conn.Query<PgDateTimeOffset>("select Timestamp from mytable; ")
//     .ToList();

var serviceCollection = new ServiceCollection();
serviceCollection.AddPooledDbContextFactory<WhatsNewContext>(builder =>
    builder.UseSqlite("Data Source=test.db")
        .LogTo(Console.WriteLine, LogLevel.Information));
var serviceLifetime = serviceCollection.Where(sd => sd.ServiceType == typeof(WhatsNewContext))
    .FirstOrDefault()
    ?.Lifetime;
serviceCollection.AddScoped(sp => sp.GetRequiredService<IDbContextFactory<WhatsNewContext>>().CreateDbContext());
Console.WriteLine(serviceLifetime);
var serviceProvider = serviceCollection.BuildServiceProvider();

WhatsNewContext ctx = null!;

// using (var scope = serviceProvider.CreateScope())
// {
//     ctx = scope.ServiceProvider.GetRequiredService<WhatsNewContext>();
//
// }

// var dbContextFactory = serviceProvider.GetRequiredService<IDbContextFactory<WhatsNewContext>>();
// var whatsNewContext = dbContextFactory.CreateDbContext();
// whatsNewContext.Database.EnsureDeleted();
// whatsNewContext.Dispose();

using (var scope = serviceProvider.CreateScope())
{
    ctx = scope.ServiceProvider.GetRequiredService<WhatsNewContext>();
    ctx.Database.EnsureDeleted();
    ctx.Database.EnsureCreated();

    ctx.SaveChanges();
    // ctx.Models.FromSqlInterpolated($"select * from Model where 1 = {1};");
int i = 1234;

var first = ctx.Database.SqlQuery<string>($"select hex({i}) as Value").First();
    ctx.Models.Add(new Model
    {
        Status = Status.Active,
    });
    var entityEntries = ctx.ChangeTracker.Entries<BaseModel>();
    ctx.SaveChanges();


    ctx.Models.Add(new Model
    {
        Status = Status.Active,
    });
    var exp = new Expense
    {
        Id = Guid.NewGuid()
    };
    var expState = new ExpenseState
    {
        State = "Pending",
        ExpenseId = exp.Id,
    };

    exp.States.Add(expState);
    ctx.Expense.Add(exp);
    ctx.SaveChanges();

    exp.LatestState = expState;
    ctx.SaveChanges();
    ctx.ChangeTracker.Clear();
    var models = ctx.Expense
        .Include(e =>e.States)
        .Include(e => e.LatestState)
        .ToList();
}
using (var scope = serviceProvider.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<WhatsNewContext>();
}


// SELECT "p"."Id", "p"."CatalogPrice", "p"."Code", "p"."CustomerProductInfoId", "p"."Name", "c"."Id", "c"."CustomerCode", "c"."CustomerPrice", "c"."ProductCode"
// FROM "Products" AS "p"
// LEFT JOIN "CustomerProducts" AS "c" ON "p"."CustomerProductInfoId" = "c"."Id"
// WHERE "c"."CustomerCode" IS NULL

// SELECT "p"."Id", "p"."CatalogPrice", "p"."Code", "p"."CustomerProductInfoId", "p"."Name", "c"."Id", "c"."CustomerCode", "c"."CustomerPrice", "c"."ProductCode"
// FROM "Products" AS "p"
// LEFT JOIN "CustomerProducts" AS "c" ON "p"."CustomerProductInfoId" = "c"."Id"
// WHERE "c"."CustomerCode" IS NULL AND "c"."Id" IS NULL



public class WhatsNewContext : DbContext
{
    public WhatsNewContext(DbContextOptions<WhatsNewContext> opts) : base(opts)
    {
    }

    // public DbSet<Author> Authors { get; set; }
    public DbSet<Model> Models => Set<Model>();
    public DbSet<Expense> Expense => Set<Expense>();
    public DbSet<ExpenseState> ExpenseState => Set<ExpenseState>();


    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Model>()
            .ComplexProperty(m => m.ComplexType
                , b => b.Property(c => c.Name)
                    .HasColumnName("Name"));

        // builder.Entity<StatusLookup>()
        //     .ToTable($"Lookup_Status")
        //     .HasKey(t => t.Id);
        // builder.Entity<StatusLookup>()
        //     .Property(t => t.Code)
        //     .HasMaxLength(64)
        //     .IsRequired();
        // builder.Entity<StatusLookup>()
        //     .HasIndex(s => s.Code)
        //     .IsUnique();
        //
        // // Seed the lookup table
        // var data = Enum.GetValues<Status>()
        //     .Select( v => new StatusLookup
        //         {
        //             Id = v,
        //             Code = typeof(Status)
        //                 .GetMember(v.ToString("G"))
        //                 .FirstOrDefault()
        //                 ?.GetCustomAttribute<CodeAttribute>()?.Code 
        //                    ?? v.ToString("G")
        //         }
        //     )
        //     .ToList();
        // builder.Entity<StatusLookup>()
        //     .HasData(data);
        //
        // // I know this doesn't work, but should signify what I am trying to do
        // builder.Entity<Model>()
        //     .HasOne<StatusLookup>()
        //     .WithMany()
        //     // Ideally EF Core would know this is mapping to a string column in the database due
        //     // to the previous definition saying that it has a string conversion, but this does not work.
        //     .HasForeignKey(t => t.Status)
        //     .OnDelete(DeleteBehavior.Restrict);


    }
}

public class Author
{
    public int Id { get; set; }
    public Model Type { get; set; }
}

public class PgDateTimeOffset
{
    public PgDateTimeOffset()
    {
        
    }
    public PgDateTimeOffset(DateTime dateTimeUtc, short offset)
    {
        DateTimeUtc = DateTime.SpecifyKind(dateTimeUtc, DateTimeKind.Utc);
        Offset = offset;
    }

    public DateTime DateTimeUtc { get; set; }
    public short Offset { get; set; }
}

public class Model : BaseModel
{
    public int Id { get; set; }
    public Status Status { get; set; }
    public ComplexType ComplexType { get; set; }
    
}

public class BaseModel{}

public class ComplexType
{
    public string Name { get; set; }
    public string Data { get; set; }
}
public enum Status
{
    [Code("Active")]
    Active = 1,

    [Code("Pending")]
    Pending = 2,

    [Code("InProgress")]
    InProgress = 3,

    [Code("Complete")]
    Complete = 4
}

public class CodeAttribute : Attribute
{
    public string Code { get; }

    public CodeAttribute(string code)
    {
        Code = code;
    }
}

public class StatusLookup
{
    public required Status Id { get; init; }
    public required string Code { get; init; }
}

public class Expense
{
    public Guid Id { get; set; }
    public ICollection<ExpenseState> States { get; } = [];
    
    [ForeignKey(nameof(LatestState))]
    public Guid? LatestStateId { get; set; }
    public ExpenseState? LatestState { get; set; }
}

public class ExpenseState
{
    public Guid Id { get; set; }
    public Guid ExpenseId { get; set; }
    public string State { get; set; }
}

class Left
{
    public List<Right> Right { get; set; }
}

class Right
{
    
    public List<Left> Lefts { get; set; }
}

class MyClass : IEntityTypeConfiguration<Right>
{
    public void Configure(EntityTypeBuilder<Right> builder)
    {
        builder.HasMany(s => s.Lefts)
            .WithMany(sp => sp.Right)
            .UsingEntity(
                "SwitchSoundProfiles",
                j => j
                    .HasOne(typeof(Left))
                    .WithMany()
                    .HasForeignKey("SoundProfileId")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasOne(typeof(Right))
                    .WithMany()
                    .HasForeignKey("SwitchId")
                    .OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.ToTable("SwitchSoundProfiles");
                    j.HasKey("SwitchId", "SoundProfileId");
                    j.Navigation("SoundProfileId").IsRequired(false);
                    j.Navigation("SwitchId").IsRequired(false);
                }
            );
    }
}
