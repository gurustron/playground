using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;

namespace NET8Benchs;


[MemoryDiagnoser]
public class QueryTrackingBehaviorBench
{
    [Params(10)]
    public int NumBlogs { get; set; }

    [Params(300)]
    public int NumPostsPerBlog { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        Console.WriteLine("Setting up database...");
        using var context = new BloggingContext();
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        BloggingContext.SeedData(NumBlogs, NumPostsPerBlog);
        Console.WriteLine("Setup complete.");
    }

    [Benchmark(Baseline = true)]
    public List<Post> AsTracking()
    {
        using var context = new BloggingContext();

        return context.Posts.AsTracking().Include(p => p.Blog).ToList();
    }

    [Benchmark]
    public List<Post> AsNoTracking()
    {
        using var context = new BloggingContext();

        return context.Posts.AsNoTracking().Include(p => p.Blog).ToList();
    }
    
    [Benchmark]
    public List<Post> AsNoTrackingWithIdentityResolution()
    {
        using var context = new BloggingContext();

        return context.Posts.AsNoTrackingWithIdentityResolution().Include(p => p.Blog).ToList();
    }

    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite($"Filename={nameof(BloggingContext)}.db");

        public static void SeedData(int numBlogs, int numPostsPerBlog)
        {
            using var context = new BloggingContext();
            context.AddRange(
                Enumerable.Range(0, numBlogs).Select(
                    _ => new Blog { Posts = Enumerable.Range(0, numPostsPerBlog).Select(_ => new Post()).ToList() }));
            context.SaveChanges();
        }
    }

    public class Blog
    {
        public int BlogId { get; set; }
        public string? Url { get; set; }
        public int Rating { get; set; }
        public List<Post> Posts { get; set; }
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; } = "Title" + Random.Shared.Next(0, 9);
        public string Content { get; set; } = "Title" + Random.Shared.Next(0, 9);

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}