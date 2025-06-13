using System.Numerics;
using System.Runtime.CompilerServices;
using ApacheIgniteSimd;
using BenchmarkDotNet.Running;

Console.WriteLine("Hello, World!");

// ref int f = ref GetSetStaticPrivateField(null);
// ref int f1 = ref GetSetPrivateField(new Class(1));
//
// [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "PrivateField")]
// extern static ref int GetSetPrivateField(Class c);
// [UnsafeAccessor(UnsafeAccessorKind.StaticField, Name = "StaticPrivateField")]
// extern static ref int GetSetStaticPrivateField(Class c);
//
// // internal readonly int _sign; // Do not rename (binary serialization)
// [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "_sign")]
// extern static ref int GetSetSign(ref MyBigInteger c);
//
// MyBigInteger x = new MyBigInteger();
// ref int iii = ref GetSetSign(ref x);
// var i2 = (BigInteger.One << 16).ToInt32TruncateCompiled();
//
// var int32TruncateUnsafeAccessor = (BigInteger.One << 16).ToInt32TruncateUnsafeAccessor();

BenchmarkRunner.Run<BigIntToIntBench>();



public class Class
{
    static void StaticPrivateMethod() { }
    static int StaticPrivateField;
    public Class(int i) { PrivateField = i; }
    void PrivateMethod() { }
    internal readonly int  PrivateField;
    int PrivateProperty { get => PrivateField; }
}

public struct MyBigInteger
{
    internal int _sign; // Do not rename (binary serialization)
}