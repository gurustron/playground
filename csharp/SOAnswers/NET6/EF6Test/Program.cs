// See https://aka.ms/new-console-template for more information

using System.ComponentModel.DataAnnotations;
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
    var ctx = scope.ServiceProvider.GetService<SomeContext>();
    var migrator = ctx.GetInfrastructure().GetService<IMigrator>();

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
    var ctx = scope.ServiceProvider.GetService<SomeContext>();
    var message = ctx.Messages.First();
    var addresses = ctx.Addresses
        .Include(a => a.SecondaryPeopleAddresses)
        .ToList();
    var listAsync = await ctx.Images
        .Where(i => !i.Messages.Any() || i.Messages.Any(m => m.MessageId == message.MessageId))
        .ToListAsync();
}

public class SomeContext : DbContext
{
    public SomeContext(DbContextOptions<SomeContext> options) : base(options)
    {
    }

    public DbSet<Message> Messages { get; set; }
    public DbSet<Image> Images { get; set; }

    public DbSet<Person> Persons { get; set; }
    public DbSet<Address> Addresses { get; set; }

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
    public ICollection<Image> Images { get; set; }
}

public class Image
{
    public int ImageId { get; set; }

    [Required] [StringLength(2048)] public string Path { get; set; }

    public virtual ICollection<Message> Messages { get; set; }
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