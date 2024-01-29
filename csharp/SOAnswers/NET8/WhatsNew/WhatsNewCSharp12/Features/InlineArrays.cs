namespace WhatsNewCSharp12.Features;

public class InlineArrays
{
    public static void Do()
    {
        var buffer = new Buffer();

        for (int i = 0; i < 9; i++)
        {
            buffer[i] = i;
        }

        buffer[0] = 42;

        foreach (var i in buffer)
        {
            Console.WriteLine(i);
        }

        Console.WriteLine(buffer);
        Console.WriteLine(buffer._element0);
        foreach (var i in buffer)
        {
            Console.WriteLine(i);
        }
    }
}

[System.Runtime.CompilerServices.InlineArray(10)]
file struct Buffer
{
    public int _element0;
}

[System.Runtime.CompilerServices.InlineArray(100)]
file struct CharBuffer
{
    public char _element0;
}