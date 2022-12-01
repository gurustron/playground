using System.Numerics;
using NET7Console.GenericMathPlayground;
using NET7Console.SystemTextJsonTests;

Console.WriteLine("Hello, World!");

GenSum(1, 2);
var res = GenSum(1, 2.0);
var add15 = Add15(1.0);
Console.WriteLine(3/4);
Console.WriteLine(NonIntegerDivision(3,4));
StaticAbstractInterfacesStuff.Do();
ReadOnlySpan<Char> s1 = "true false"[0..4];
ReadOnlySpan<Char> s2 = "true";
Console.WriteLine(s1 == s2);
Console.WriteLine();

T GenSum<T>(T l, T r) where T : IAdditionOperators<T, T, T> => l + r;
T GenSumNumber<T>(T l, T r) where T : INumber<T> => l + r;
T AddOne<T>(T n) where T : INumber<T> => T.One + n;
T Add15<T>(T n) where T : INumber<T> => T.CreateChecked(15) + n;
double NonIntegerDivision<T>(T d, T d1) where T : IBinaryInteger<T> =>  Convert.ToDouble(d) / Convert.ToDouble(d1);

void Shifts()
{
    Console.WriteLine(8.ToString("x8"));
    Console.WriteLine((-8).ToString("x8"));
    Console.WriteLine();
    Console.WriteLine((8 >> 2).ToString("x8"));
    Console.WriteLine((-8 >> 2).ToString("x8"));
    Console.WriteLine();
    Console.WriteLine((8 >>> 2).ToString("x8"));
    Console.WriteLine((-8 >>> 2).ToString("x8"));
}