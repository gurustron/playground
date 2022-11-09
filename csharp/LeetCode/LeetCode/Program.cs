using System;
using LeetCode;
using LeetCode.Study.DataStructure.DataStructureOne;

Console.WriteLine("Hello World!");
// new ReshapetheMatrix().MatrixReshape(new []{new[]{1,2},new[]{3,4}}, 4, 1);
var generate = new PascalsTriangle().Generate(5);
Console.WriteLine();
// var solution = new MaximumSubarraySolution().MaxSubArray(new []{-2,1,-3,4,-1,2,1,-5,4});
// Console.WriteLine(solution);

// var solution = new MoveZeroes.Solution();
//
// var tries = new[]
// {
//     new[] { 9 },
//     new[] { 0, 9, 0, 0, 0 },
//     new[] { 0, 0, 0, 0 },
//     new[] { 0, 0, 0, 1 },
// };
//
// foreach (var nums in tries)
// {
//     solution.MoveZeroes(nums);
//     PrintResult(nums);
// }

void PrintResult(int[] array) => Console.WriteLine(array.ToPrintVersion());

