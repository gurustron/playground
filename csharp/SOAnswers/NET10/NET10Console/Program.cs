// See https://aka.ms/new-console-template for more information

using System.Buffers;
using System.Text.Json;

using System.Diagnostics;
using System.Text.RegularExpressions;


var ssv = SearchValues.Create(["one", "two"], StringComparison.OrdinalIgnoreCase);

var r = new[] { "test", "this contains one", "one" }.AsSpan().IndexOfAny(ssv);
var ofAny = "this contains one".AsSpan().IndexOfAny(ssv);
var ofAny1 = "this contains one".IndexOfAny(ssv);

Match m = Regex.Match("abc", "(?=(abc))");
Debug.Assert(m.Success);

foreach (Group g in m.Groups)
{
    foreach (Capture c in g.Captures)
    {
        Console.WriteLine($"Group: {g.Name}, Capture: {c.Value}");
    }
}

Console.WriteLine();
// Console.WriteLine("Hello, World!");
//
// var left = new[] { (1, "Hello"), (2, "World") };
// var right = new[] { new { Id = 2 } };
//
// var result = left
//     .LeftJoin(right, 
//         t => t.Item1,
//         t => t.Id,
//         (l, r) => new {Value = l.Item2, Matched = r is not null })
//     .ToArray();
//
// Console.WriteLine(JsonSerializer.Serialize(result));