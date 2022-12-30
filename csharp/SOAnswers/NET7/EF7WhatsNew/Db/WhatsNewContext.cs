using Microsoft.EntityFrameworkCore;

namespace EF7WhatsNew.Db;

public class WhatsNewContext:DbContext
{
    public WhatsNewContext(DbContextOptions<WhatsNewContext> opts):base(opts)
    {
    }

    public DbSet<Author> Authors => Set<Author>();
}