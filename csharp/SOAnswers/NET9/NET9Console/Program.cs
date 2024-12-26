// See https://aka.ms/new-console-template for more information

using System.Numerics;

Console.WriteLine("Hello, World!");
int i = 0;
var totalAllocatedBytes = GC.GetTotalAllocatedBytes(true);
Console.WriteLine(new object());
for (int j = 0; j < 10_000; j++)
{
    i = 100L.ToNumberOrDefault(1);
}
for (int j = 0; j < 10_000; j++)
{
    i = 100.ToNumberOrDefault(1);
}

Console.WriteLine(GC.GetTotalAllocatedBytes(true) - totalAllocatedBytes);

public static class Exts
{

    public static TDestination ToNumberOrDefault<TSource, TDestination>(this TSource number, TDestination defaultValue)
        where TSource : struct, INumber<TSource>
        where TDestination : struct, INumber<TDestination>
    {
        try
        {
            return TDestination.CreateChecked(number);
        }
        catch
        {
            return defaultValue;
        }
    }
}

public class C
{
    public bool M<TOther>(TOther value) 
    {
        if (typeof(TOther) == typeof(long))
        {
            long actualValue = (long)(object)value;
            Console.WriteLine(actualValue);
            return true;
        }

        return false;

    }
}