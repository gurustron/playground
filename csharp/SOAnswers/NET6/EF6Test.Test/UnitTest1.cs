using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace EF6Test.Test;

public class Tests
{
    record TestDataWithListOfChildren(string Name, IList<TestSubData> Children, long Id = default)
    {
        public TestDataWithListOfChildren() : this(default, default) { }
    };

    record TestDataWithTwoChildren(string Name, TestSubData Child1, TestSubData Child2, long Id = default)
    {
        public TestDataWithTwoChildren() : this(default, default, default) { }
    };

record TestSubData(string Name, long Id = default)
{
// for DB records we need a default constructor without parameters
public TestSubData() : this(default) { }

public ICollection<TestDataWithListOfChildren> ParentTestDatas = new List<TestDataWithListOfChildren>();
};

    class TestDb : DbContext
    {
        public DbSet<TestDataWithListOfChildren> TestDataWithListOfChildren { get; set; } = default!;
        public DbSet<TestSubData> TestSubData { get; set; } = default!;
        public DbSet<TestDataWithTwoChildren> TestDataWithTwoChildren { get; set; } = default!;

        public TestDb() : base(new DbContextOptionsBuilder()
            // .UseSqlite($"Filename={nameof(TestDb)}.db")
            .UseInMemoryDatabase("testDb", new InMemoryDatabaseRoot())
            .LogTo(i => Debug.WriteLine(i), LogLevel.Information).Options) 
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestDataWithListOfChildren>()
                .HasMany(t => t.Children)
                .WithMany(t => t.ParentTestDatas);
        }
    }

    [Test]
    public async Task Test_WithListOfChildren()
    {
        var dictionary = new Dictionary<int, string>
        {
            [1] = ""
        };
        // Create DB
        using TestDb testDb = new();

        testDb.Database.EnsureDeleted();
        testDb.Database.EnsureCreated();
        // define data
        var sharedChild = new TestSubData("Shared", 1);
        var entry1 = new TestDataWithListOfChildren("Max1", 
            new List<TestSubData> {
                sharedChild,
                new TestSubData("unique #1",2)
            });
        var entry2 = new TestDataWithListOfChildren("Max2",
            new List<TestSubData> {
                sharedChild,
                new TestSubData("unique #2",3)
            });

        // now add the data, but try to replace existing sub-data with already existing entries
        testDb.Add(entry1);

        // just to make sure
        await testDb.SaveChangesAsync();
        var data1 = testDb.TestDataWithListOfChildren.Include(x => x.Children).ToArray();
        Assert.That(data1[0].Children.Count, Is.EqualTo(2));    // everything still good

        testDb.Add(entry2);

        // Save DB
        await testDb.SaveChangesAsync();

        // ReadData and assert
        Assert.That(testDb.TestDataWithListOfChildren.Count(), Is.EqualTo(2));  // 2x TestData
        Assert.That(testDb.TestSubData.Count(), Is.EqualTo(3));  // 3x TestSubData

        // why is the existing data modified when adding another entry
        Assert.That(data1[0].Children.Count, Is.EqualTo(2));        // acutal result = 1; why is the shared child gone

        var data2 = testDb.TestDataWithListOfChildren.Include(x => x.Children).ToArray();
        Assert.That(data2[0].Children.Count, Is.EqualTo(2));        // acutal result = 1; why is the shared child gone
        Assert.That(data2[1].Children.Count, Is.EqualTo(2));
    }

    [Test]
    public async Task Test_TestDataWithTwoChildren()
    {
        // Create DB
        using TestDb testDb = new();

        // define data
        var sharedChild = new TestSubData("Shared", 1);
        var entry1 = new TestDataWithTwoChildren("Max1",
            sharedChild,
            new TestSubData("unique #1",2)
        );
        var entry2 = new TestDataWithTwoChildren("Max2",
            sharedChild,
            new TestSubData("unique #2",3)
        );

        // now add the data, but try to replace existing sub-data with already existing entries
        testDb.Add(entry1);
        testDb.Add(entry2);

        // Save DB
        await testDb.SaveChangesAsync();

        // ReadData and assert
        Assert.That(testDb.TestDataWithTwoChildren.Count(), Is.EqualTo(2));  // 2x TestData
        Assert.That(testDb.TestSubData.Count(), Is.EqualTo(3));  // 3x TestSubData

        // This works
        var data2 = testDb.TestDataWithTwoChildren.Include(x => x.Child1).Include(x => x.Child2).ToArray();
        Assert.NotNull(data2[0].Child1);
        Assert.NotNull(data2[0].Child2);
        Assert.NotNull(data2[1].Child1);
        Assert.NotNull(data2[1].Child2);
    }
}