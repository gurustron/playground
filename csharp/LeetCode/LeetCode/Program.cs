using System;
using LeetCode;

Console.WriteLine("Hello World!");

var data = new []
{
    // (2, new[]{1,2,3}),
    (2, new []{-1,-100,3,99})
};

foreach (var (k, ints) in data)
{
    new RotateArray().Rotate(ints, k);
    Console.WriteLine(string.Join(',', ints));
}
