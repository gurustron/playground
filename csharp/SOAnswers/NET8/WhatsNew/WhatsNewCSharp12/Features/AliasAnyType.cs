global using MyPointGlobal = (int X, int Y);

namespace WhatsNewCSharp12.Features;

using MyPoint = (int X, int Y);

public class AliasAnyType
{
    public static void Do()
    {
        Console.WriteLine(nameof(MyPoint));
        Console.WriteLine(typeof(MyPoint));
        Console.WriteLine(typeof(MyPoint).FullName);
        Console.WriteLine(nameof(MyPointGlobal));
        Console.WriteLine(typeof(MyPointGlobal));
    }
}