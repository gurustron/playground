namespace ASPNET6Test;

public static class BuilderExts
{
    public static TBuilder RequireCustomAuth<TBuilder>(this TBuilder builder, string meta) where TBuilder : IEndpointConventionBuilder
    {
        if (builder == null)
        {
            throw new ArgumentNullException(nameof(builder));
        }
        // var enumerable = Task.FromResult(new List<int>()).GetAsyncEnumerable();
        return builder.WithMetadata(new SomeCustomMeta { Meta = meta });
    }
}

public static class EnumerableExtensions
{
    public static async IAsyncEnumerable<T> GetAsyncEnumerable<T>(this Task<IEnumerable<T>> task)
    {
        foreach (var item in await task)
        {
            yield return item;
        }
    }
}