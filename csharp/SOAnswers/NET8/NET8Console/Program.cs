using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");


async Task TestTask(Func<Task> factory)
{
    Console.WriteLine("Before task");
    var t = factory();
    Console.WriteLine("Task created");
    await t;
    Console.WriteLine("After awaiting task");
}
Console.WriteLine("---------------");
await TestTask(async () =>
{
    Console.WriteLine("    before await");
    await Task.CompletedTask;
    Console.WriteLine("    after await");
});

await TestTask(async () =>
{
    Console.WriteLine("    before await");
    await Task.Yield();
    Console.WriteLine("   after await");
});

var ctx = new TestContext();
ctx.Database.EnsureDeleted();
ctx.Database.EnsureCreated();
ctx.Items.ToArray();
ctx.Items.Add(new() { Id = 1, Name = "as", Children = new(){
    new(){Identificator = 100, Name="asd"}
}});

ctx.SaveChanges();
ctx.ChangeTracker.Clear();
Console.WriteLine(ctx.Items.First().Children.First().Identificator);

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=test.db");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TestItem>().OwnsMany(ti => ti.Children, builder => builder.ToJson());
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