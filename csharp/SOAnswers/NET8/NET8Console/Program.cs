using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

Console.WriteLine("Hello, World!");

var ctx = new TestContext();
ctx.Database.EnsureDeleted();
ctx.Database.EnsureCreated();
ctx.Items.ToArray();
ctx.Items.Add(new() { Id = 1, Name = "as", Children = new(){
    new(){Identificator = 100, Name="asd"}
}});

var org = new Organization
{
    Name = "Test1"
};

ctx.Users.Add(new User()
{
    Following = new()
    {
        org,
        new Organization
        {
            Name = "Foo"
        }
    }
});

ctx.Users.Add(new User()
{
    Following = new()
    {
        org
    }
});
ctx.SaveChanges();
ctx.ChangeTracker.Clear();
Console.WriteLine(ctx.Items.First().Children.First().Identificator);
var users = ctx.Users.Include(user => user.Following).ToList();
Console.WriteLine();

public class SomeClass
{
    private readonly Person _person;

    public required Person Person
    {
        get => _person;
        [MemberNotNull(nameof(_person))]
        init
        {
            ArgumentNullException.ThrowIfNull(value);
            _person = value;
        }
    }

}

public class Person
{
}

class TestContext : DbContext
{
    public DbSet<TestItem> Items { get; set; }
    public DbSet<Organization> Organizations => Set<Organization>();
    public DbSet<User> Users => Set<User>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=test.db");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.Entity<TestItem>().OwnsMany(ti => ti.Children, builder => builder.ToJson());
    }
}

public class Organization {
    public int Id { get; set; }

    public string Name { get; set; }
    // lots of properties
// NO List<User> Followers { get; set; }
}

public class User {
    public int Id { get; set; }
// lots of properties
    public List<Organization> Following { get; set; }
}

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasMany(u => u.Following)
            .WithMany();
    }
}
class TestItem
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required List<TestChild> Children { get; set; }
}

class TestChild
{
    public required int Identificator { get; set; }
    public required string Name { get; set; }
}