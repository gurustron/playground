// See https://aka.ms/new-console-template for more information

using System.Text.Json;

Console.WriteLine("Hello, World!");

var left = new[] { (1, "Hello"), (2, "World") };
var right = new[] { new { Id = 2 } };

var result = left
    .LeftJoin(right, 
        t => t.Item1,
        t => t.Id,
        (l, r) => new {Value = l.Item2, Matched = r is not null })
    .ToArray();

Console.WriteLine(JsonSerializer.Serialize(result));