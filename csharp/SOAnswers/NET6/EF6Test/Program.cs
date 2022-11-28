// See https://aka.ms/new-console-template for more information

using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Hello, World!");

var serviceCollection = new ServiceCollection();
serviceCollection.AddDbContext<SomeContext>(builder => 
    builder.UseSqlite($"Filename={nameof(SomeContext)}.db", opts => opts.CommandTimeout(1000) )
        .LogTo(Console.WriteLine), ServiceLifetime.Transient);
var serviceProvider = serviceCollection.BuildServiceProvider();

using (var scope = serviceProvider.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<SomeContext>();
    var migrator = ctx.GetInfrastructure().GetRequiredService<IMigrator>();

    migrator.Migrate();
    ctx.Database.EnsureDeleted();
    ctx.Database.EnsureCreated();
    ctx.Persons.Add(new Person { PrimaryAddress = new Address(), SecondaryAddress = new Address()});
    ctx.Persons.Add(new Person { PrimaryAddress = new Address()});
    ctx.Addresses.Add(new Address());
    ctx.Images.Add(new Image
    {
        Path = "qwerty"
    });
    ctx.Messages.Add(new Message
    {
        MessageText = "qwerty",
        Images = new List<Image>
        {
            new()
            {
                Path = "qwerty"
            }
        }
    });
    ctx.SaveChanges();
}

using (var scope = serviceProvider.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<SomeContext>();
    
    var message = ctx.Messages.First();
    message.MessageText = "'asdads:";
    var modified = ctx.Entry(message).Members.Where(m => m.IsModified)
        .ToList();
    var addresses = ctx.Addresses
        .Include(a => a.SecondaryPeopleAddresses)
        .ToList();
    var listAsync = await ctx.Images
        .Where(i => !i.Messages.Any() || i.Messages.Any(m => m.MessageId == message.MessageId))
        .ToListAsync();
}
Console.WriteLine(Domains.GetRole(1));
GetAllBySampleUniqueId(1);
GetAllBySampleUniqueId(2);

Address? GetAllBySampleUniqueId(int uniqueId)
{

    using (SomeContext db = new(new DbContextOptions<SomeContext>()))
    {
        var dbSample = db.Persons.Include(x => x.PrimaryAddress)
            .Include(x => x.SecondaryAddress)
            .FirstOrDefault(x => x.SecondaryAddressId == uniqueId);

        if (dbSample == null)
        {
            return null;
        }

        var dbResults = dbSample.PrimaryAddress;
        return dbResults;
    }
}


    class Item
{
    
}

class Weapon: Item
{
    
}
static class Domains
{
    public static DomainRole GetRole(ushort? role)
    {

        IEnumerable<int> x = new Queue<int>();

        Parallel.ForEachAsync(x, new ParallelOptions
        {
            MaxDegreeOfParallelism = 3
        }, async (i, token) => await Task.Delay(i * 1000, token));
        

        return role switch
        {
            >= 0 and <= 5 => (DomainRole)role,
            _             => DomainRole.Unknown
        };
    }
}

enum DomainRole
{
    Unknown                 = -1,
    StandaloneWorkstation   = 0,
    MemberWorkstation       = 1,
    StandaloneServer        = 2,
    MemberServer            = 3,
    BackupDomainController  = 4,
    PrimaryDomainController = 5
}

public class SomeContext : DbContext
{
    public SomeContext(DbContextOptions<SomeContext> options) : base(options)
    {
    }

    public DbSet<Message> Messages => Set<Message>();
    public DbSet<Image> Images => Set<Image>();

    public DbSet<Person> Persons => Set<Person>();
    public DbSet<Address> Addresses => Set<Address>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>()
            .HasOne(person => person.PrimaryAddress)
            .WithMany(a => a.PrimaryPeopleAddresses);

        modelBuilder.Entity<Person>()
            .HasOne(person => person.SecondaryAddress)
            .WithMany(a => a.SecondaryPeopleAddresses);
    }
}

public class Message
{
    public int MessageId { get; set; }
    public string MessageText { get; set; }
    public ICollection<Image> Images { get; set; } = new HashSet<Image>();
}

public class Image
{
    public int ImageId { get; set; }

    [Required] [StringLength(2048)] public string Path { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = new HashSet<Message>();
}

    public class Person
    {
        public int Id { get; set; }

        [Required] 
        public int? PrimaryAddressId { get; set; }
        public Address? PrimaryAddress { get; set; }

        public int? SecondaryAddressId { get; set; }
        public Address? SecondaryAddress { get; set; }
    }

    public class Address
    {
        public int Id { get; set; }
        public List<Person> PrimaryPeopleAddresses { get; set; } = new();
        public List<Person> SecondaryPeopleAddresses { get; set; } = new();
    }

