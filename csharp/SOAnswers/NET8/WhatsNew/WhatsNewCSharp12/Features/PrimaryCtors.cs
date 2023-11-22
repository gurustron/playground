using System.Reflection;

namespace WhatsNewCSharp12.Features;

public class PrimaryCtors
{
    public static void Do()
    {
        new CtorLess();
        
        Console.WriteLine("-------------");
        Console.WriteLine(new Distance());
        var fieldInfos = typeof(Distance).GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (var fieldInfo in fieldInfos)
        {
            Console.WriteLine(fieldInfo.Name);
        }
        Console.WriteLine("-------------");

        Console.WriteLine(new DistanceClass(1, 2, 3));
        fieldInfos = typeof(Distance).GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (var fieldInfo in fieldInfos)
        {
            Console.WriteLine(fieldInfo.Name);
        }
        Console.WriteLine("-------------");
    }
}

public class CtorLess; // no parameters
public readonly struct Distance(double dx, double dy, double dz)
{
    public readonly double Magnitude { get; } = Math.Sqrt(dx * dx + dy * dy);
    public readonly double Direction { get; } = Math.Atan2(dy, dx);

    public void Do() => Console.WriteLine(dx + 1);
}

public class DistanceClass(double dx, double dy, double dz)
{
    public double Magnitude { get; } = Math.Sqrt(dx * dx + dy * dy);
    public double Direction { get; } = Math.Atan2(dy, dx);

    public void Do() => Console.WriteLine(dx + 1);
}