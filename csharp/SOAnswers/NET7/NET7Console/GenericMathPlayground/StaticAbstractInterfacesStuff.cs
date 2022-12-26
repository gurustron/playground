using System.Reflection;

namespace NET7Console.GenericMathPlayground;

public class StaticAbstractInterfacesStuff
{
    public static void Do()
    {
        List<object> l = new()
        {
            new ItemOne(),
            new ItemTwo()
        };
        foreach (var haveStatic in l)
        {
            var propertyInfos = haveStatic.GetType().GetProperties(BindingFlags.Static | BindingFlags.Public);
        }

        var discriminator = ItemOne.Discriminator;
        var disc = GetDisc(new ItemOne());
    }

    public static string GetDisc<T>(T item) where T : IHaveStatic => T.Discriminator;
}

public interface IHaveStatic
{
    public static abstract string Discriminator { get; }
    string GetDiscriminator() => "as";
}

file class ItemOne : IHaveStatic
{
    public static string Discriminator => nameof(ItemOne);
}

file class ItemTwo : IHaveStatic
{
    public static string Discriminator => nameof(ItemTwo);
}