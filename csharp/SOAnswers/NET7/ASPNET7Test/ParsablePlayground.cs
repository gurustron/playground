using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace ASPNET7Test;

public class ParsablePlayground
{
    
class MyInt : IParsable<int>
{
    public static int Parse(string s, IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public static bool TryParse(string? s, IFormatProvider? provider, out int result)
    {
        throw new NotImplementedException();
    }

    public static void X()
    {
        // MyInt.Do(new MyClass());
        // var x = InvariantParse<MyClass>("");
        // var x1 = MyInt.InvariantParse<MyC, MyClass1>("");
    }
    public static void Do<T>(T t) where T : IParsable<T>
    {
           
    }
    
    static T InvariantParse<T>(string s)
        where T : IParseable1<T>
    {
        return T.Parse(s, CultureInfo.InvariantCulture);
    }
    
public static TTO InvariantParseTwoGenerics<T, TTO>(string s)
    where T : IParseable1<TTO>
{
    return T.Parse(s, CultureInfo.InvariantCulture);
}

    
}

public interface IParseable1<TSelf>
    // where TSelf : IParseable1<TSelf>
{
    static abstract TSelf Parse(string s, IFormatProvider? provider);

    static abstract bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, out TSelf result);
}

class MyClass1
{
}

class MyC : IParseable1<MyClass1>
{
    public static MyClass1 Parse(string s, IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, out MyClass1 result)
    {
        throw new NotImplementedException();
    }
}
}