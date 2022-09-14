namespace ASPNET6Test;

public static class BuilderExts
{
    public static TBuilder RequireCustomAuth<TBuilder>(this TBuilder builder, string meta) where TBuilder : IEndpointConventionBuilder
    {
        if (builder == null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        return builder.WithMetadata(new SomeCustomMeta { Meta = meta });
    }
}