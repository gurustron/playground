using System;
using LeetCode;

Console.WriteLine("Hello World!");

var solution = new MoveZeroes.Solution();

var tries = new[]
{
    new[] { 9 },
    new[] { 0, 9, 0, 0, 0 },
    new[] { 0, 0, 0, 0 },
    new[] { 0, 0, 0, 1 },
};

foreach (var nums in tries)
{
    solution.MoveZeroes(nums);
    PrintResult(nums);
}

void PrintResult(int[] array) => Console.WriteLine(array.ToPrintVersion());

