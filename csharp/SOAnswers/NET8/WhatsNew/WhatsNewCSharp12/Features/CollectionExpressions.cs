using System.Collections;
using System.Runtime.CompilerServices;

namespace WhatsNewCSharp12.Features;

public class CollectionExpressions
{
    public static void Do()
    {
        // Create an array:
        int[] a = [1, 2, 3, 4, 5, 6, 7, 8];

        // Create a list:
        List<string> b = ["one", "two", "three"];


        // Create a span
        Span<char> c = ['a', 'b', 'c', 'd', 'e', 'f', 'h', 'i'];

        // Create a jagged 2D array:
        int[][] twoD = [[1, 2, 3], [4, 5, 6], [7, 8, 9]];

        // Create a jagged 2D array from variables:
        int[] row0 = [1, 2, 3];
        int[] row1 = [4, 5, 6];
        int[] row2 = [7, 8, 9];
        int[][] twoDFromVariables = [row0, row1, row2];

        List<List<int>> jaggedListEmpty = [];
        List<List<int>> jaggedList = [[1], [1, 2]];

        IEnumerable<IEnumerable<int>> jaggedEnumerableEmpty = [];
        IEnumerable<IEnumerable<int>> jaggedEnumerable = [[]];

        int[] single = [..row0, ..row1[1..2], ..row2];
        foreach (var element in single)
        {
            Console.Write($"{element}, ");
        }
        // output:
        // 1, 2, 3, 4, 5, 6, 7, 8, 9,
        
        string[] vowels = ["a", "e", "i", "o", "u"];
        string[] consonants = ["b", "c", "d", "f", "g", "h", "j", "k", "l", "m",
            "n", "p", "q", "r", "s", "t", "v", "w", "x", "z"];
        string[] alphabet = [.. vowels[2..4], .. consonants[4..^4].Reverse(), "y"];
        
        foreach (var element in alphabet)
        {
            Console.Write($"{element}, ");
        }

        MyCollectionExpressionable x = [];
        MyCollectionExpressionable x2 = ['c'];
        MyCollectionExpressionable x3 = ['c', 'a'];
        MyCollectionExpressionable x4 = ['c', 'a', 'd'];

        Console.WriteLine(x);
        Console.WriteLine(x2);
        Console.WriteLine(x3);
        Console.WriteLine(x4);
    }
}

[CollectionBuilder(typeof(MyCollectionExpressionable), "Create")]
class MyCollectionExpressionable(char? first, char? second) //: IEnumerable<char?> // actually duck typed, only `GetEnumerator` is needed
{

    public override string ToString() => $"{first}:{second}";

    public IEnumerator<char?> GetEnumerator()
    {
        yield return first;
        yield return second;
    }

    // IEnumerator IEnumerable.GetEnumerator()
    // {
    //     return GetEnumerator();
    // }

    public static MyCollectionExpressionable Create(ReadOnlySpan<char?> values) => values switch
    {
        [var single] => new MyCollectionExpressionable(single, null),
        [var first, var second, ..] => new MyCollectionExpressionable(first, second),
        _ => new MyCollectionExpressionable(null, null)
    };
}