using Microsoft.EntityFrameworkCore;

namespace EF7WhatsNew.Db.Blogginng;

public class WhatsNewContext:DbContext
{
    public WhatsNewContext(DbContextOptions<WhatsNewContext> opts):base(opts)
    {
    }

    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Blog> Blogs => Set<Blog>();
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<Tag> Tags => Set<Tag>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<Post>()
        //     .UpdateUsingStoredProcedure(
        //         "Post_Update",
        //         builder =>
        //         {
        //             builder.HasOriginalValueParameter(person => person.Id);
        //             builder.HasParameter(post => post.Title);
        //             builder.HasParameter(post => post.Content);
        //             builder.HasParameter(post => post.AuthorId);
        //             builder.HasParameter("BlogId");
        //             builder.HasParameter(post => post.PublishedOn);
        //         });
        // modelBuilder.Entity<Author>()
        //     .OwnsOne(
        //             author => author.Contact,
        //             ownedNavigationBuilder =>
        //             {
        //                 ownedNavigationBuilder.OwnsOne(contactDetails => contactDetails.Address);
        //             })
        //         // author => author.Contact, ownedNavigationBuilder =>
        //         // {
        //         //     ownedNavigationBuilder.ToJson();
        //         //     ownedNavigationBuilder.OwnsOne(contactDetails => contactDetails.Address);
        //         // })
        //     ;
    }
}