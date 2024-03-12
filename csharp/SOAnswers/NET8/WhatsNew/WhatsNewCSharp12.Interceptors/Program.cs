using System;

var c = new C();
c.InterceptableMethod(1); // (L1,C1): prints "interceptor 1"
c.InterceptableMethod(1); // (L2,C2): prints "other interceptor 1"
c.InterceptableMethod(2); // (L3,C3): prints "other interceptor 2"
c.InterceptableMethod(1); // prints "interceptable 1"

class C
{
    public void InterceptableMethod(int param)
    {
        Console.WriteLine($"interceptable {param}");
    }
}

// generated code