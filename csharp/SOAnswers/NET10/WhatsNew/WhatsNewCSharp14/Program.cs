using WhatsNewCSharp14.Features;

long.DoSomethingLong();
42L.DoSomethingInstance();
Console.WriteLine("Hello, World!");



public static class MyRecExts
{
    public static int Test(this long l) => (int)l;
}