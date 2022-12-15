using System;using System.Collections.Generic;
using System.Linq;
using LeetCode;
using LeetCode.Study.DataStructure.DataStructureOne;

Console.WriteLine("Hello World!");

var isValid = new ValidParentheses().IsValid("()");

// new MergeTwoSortedLinkedLists().MergeTwoLists(null, null);
// new MergeTwoSortedLinkedLists().MergeTwoLists(
//     new MergeTwoSortedLinkedLists.ListNode(1,
//         new MergeTwoSortedLinkedLists.ListNode(2, new MergeTwoSortedLinkedLists.ListNode(4))),
//     new MergeTwoSortedLinkedLists.ListNode(1,
//         new MergeTwoSortedLinkedLists.ListNode(3, new MergeTwoSortedLinkedLists.ListNode(4)))
// );

// var searchMatrix = new Search2DMatrix().SearchMatrix(new[] { new[] { 1, 3 } }, 3);
// new ReshapetheMatrix().MatrixReshape(new []{new[]{1,2},new[]{3,4}}, 4, 1);
// var generate = new PascalsTriangle().Generate(5);
// var f = new ValidSudoku().IsValidSudoku(new[]
// {
//     new[] { '.', '.', '4', '.', '.', '.', '6', '3', '.' }, 
//     new[] { '.', '.', '.', '.', '.', '.', '.', '.', '.' },
//     new[] { '5', '.', '.', '.', '.', '.', '.', '9', '.' }, 
//     new[] { '.', '.', '.', '5', '6', '.', '.', '.', '.' },
//     new[] { '4', '.', '3', '.', '.', '.', '.', '.', '1' },
//     new[] { '.', '.', '.', '7', '.', '.', '.', '.', '.' },
//     new[] { '.', '.', '.', '5', '.', '.', '.', '.', '.' },
//     new[] { '.', '.', '.', '.', '.', '.', '.', '.', '.' },
//     new[] { '.', '.', '.', '.', '.', '.', '.', '.', '.' }
// });
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

