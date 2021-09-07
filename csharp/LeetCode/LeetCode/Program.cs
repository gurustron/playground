using System;
using LeetCode;

Console.WriteLine("Hello World!");

var solution = new IncrementLargeDigitArray.Solution();

PrintResult(solution.PlusOne(new []{9}));
PrintResult(solution.PlusOne(new []{1,2,3,6}));
PrintResult(solution.PlusOne(new []{0}));

void PrintResult(int[] array) => Console.WriteLine(array.ToPrintVersion());

