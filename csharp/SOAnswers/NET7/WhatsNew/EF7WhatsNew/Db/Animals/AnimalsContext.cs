using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace EF7WhatsNew.Db.Animals;

public class AnimalsContext : DbContext
{
    public AnimalsContext(DbContextOptions<AnimalsContext> options) : base(options)
    {
        
    }
    
    public DbSet<Animal> Animals => Set<Animal>();
    public DbSet<Pet> Pets => Set<Pet>();
    public DbSet<FarmAnimal> FarmAnimals => Set<FarmAnimal>();
    public DbSet<Cat> Cats => Set<Cat>();
    public DbSet<Dog> Dogs => Set<Dog>();
    public DbSet<Human> Humans => Set<Human>();

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Replace<TableNameFromDbSetConvention>(provider =>
            new SnakeCaseNameConvention(
                provider.GetRequiredService<ProviderConventionSetBuilderDependencies>(), 
                provider.GetRequiredService<RelationalConventionSetBuilderDependencies>()));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FarmAnimal>().Property(e => e.Species);

        modelBuilder.Entity<Animal>().UseTpcMappingStrategy();

        modelBuilder.Entity<Human>()
            .HasMany(e => e.Pets)
            .WithMany(e => e.Humans)
            .UsingEntity<Dictionary<object, string>>(
                "PetsHumans",
                r => r.HasOne<Pet>().WithMany().OnDelete(DeleteBehavior.Cascade),
                l => l.HasOne<Human>().WithMany().OnDelete(DeleteBehavior.ClientCascade));

        base.OnModelCreating(modelBuilder);
    }
}

class SnakeCaseNameConvention : TableNameFromDbSetConvention
{
    private readonly IDictionary<Type, string> _sets;

    public SnakeCaseNameConvention(ProviderConventionSetBuilderDependencies dependencies,
        RelationalConventionSetBuilderDependencies relationalDependencies) : base(dependencies, relationalDependencies)
    {
        _sets = new Dictionary<Type, string>();
        List<Type>? ambiguousTypes = null;
        foreach (var set in dependencies.SetFinder.FindSets(dependencies.ContextType))
        {
            if (!_sets.ContainsKey(set.Type))
            {
                _sets.Add(set.Type, set.Name);
            }
            else
            {
                ambiguousTypes ??= new List<Type>();

                ambiguousTypes.Add(set.Type);
            }
        }

        if (ambiguousTypes != null)
        {
            foreach (var type in ambiguousTypes)
            {
                _sets.Remove(type);
            }
        }
    }

    public override void ProcessEntityTypeAdded(IConventionEntityTypeBuilder entityTypeBuilder, IConventionContext<IConventionEntityTypeBuilder> context)
    {
        base.ProcessEntityTypeAdded(entityTypeBuilder, context);
        if (entityTypeBuilder.Metadata.GetTableName() is { Length: > 0 } name)
        {
            entityTypeBuilder.Metadata.SetTableName(name.ToSnakeCase());
        }
    }

    public override void ProcessModelFinalizing(IConventionModelBuilder modelBuilder, IConventionContext<IConventionModelBuilder> context)
    {
        base.ProcessModelFinalizing(modelBuilder, context);
        foreach (var entityType in modelBuilder.Metadata.GetEntityTypes())
        {
            if (entityType.GetTableName() is {Length: > 0} name && _sets.ContainsKey(entityType.ClrType))
            {
                if (entityType.GetViewNameConfigurationSource() != null)
                {
                    continue;
                }

                if (entityType.GetMappingStrategy() == RelationalAnnotationNames.TpcMappingStrategy
                    && entityType.IsAbstract())
                {
                    continue;
                }

                entityType.SetTableName(name.ToSnakeCase());
            }
        }
    }
}

static class StringExts
{
    public static string ToSnakeCase(this string text)
    {
        if (text == null)
        {
            throw new ArgumentNullException(nameof(text));
        }

        if (text.Length < 2)
        {
            return text;
        }

        var sb = new StringBuilder();
        sb.Append(char.ToLowerInvariant(text[0]));
        for (int i = 1; i < text.Length; ++i)
        {
            char c = text[i];
            if (char.IsUpper(c))
            {
                sb.Append('_');
                sb.Append(char.ToLowerInvariant(c));
            }
            else
            {
                sb.Append(c);
            }
        }

        return sb.ToString();
    }
}