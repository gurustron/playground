namespace WhatsNewCSharp12.Features;

public class DefaultLambdaParams
{
    public static void Do()
    {
        // can't specify type explicitly with Func<int> x = ...
        // Func<int, int> works but x() becomes illegal
        var x = (int i = 2) => 
        {
            Console.WriteLine($"x: {i}");
            return i + 1;
        };

        x();
        x(100);

        MyFuncWithOptionalParam<int, int> xx = (int i = 42) => // 42 is ignored since the delegate has default
        {
            Console.WriteLine($"xx: {i}");
            return i + 1;
        };
        xx();
        x(100);
        
        var action = (int i = 0) => Console.WriteLine($"Action: {i}"); // void delegate(int)
        action();
        action(777);

        var sum = (params int[] values) =>
        {
            int sum = 0;
            foreach (var value in values) 
                sum += value;
    
            return sum;
        };

        var empty = sum();
        Console.WriteLine($"sum(): {empty}"); // 0

        var sequence = new[] { 1, 2, 3, 4, 5 };
        var total = sum(sequence);
        Console.WriteLine($"sum(sequence): {total}"); // 15
    }
}

delegate int MyFuncWithOptionalParam<T, TResult>(T p = default);