namespace Helpers;

public static class CollectionExts
{
    public static string ToPrintVersion<T>(this IEnumerable<T> col)
    {
        return $"[ {string.Join(", ", col)} ]";
    }

    public static void PrintResult<T>(this IEnumerable<T> array) => Console.WriteLine(array.ToPrintVersion());
}