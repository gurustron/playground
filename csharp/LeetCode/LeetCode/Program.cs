using System;using System.Collections.Generic;
using System.Linq;
using LeetCode;
using LeetCode.Study.DataStructure;
using LeetCode.Study.DataStructure.DataStructureOne;
using LeetCode.Study.DataStructure.DataStructureTwo;

ThirtyThreeSearchInRotatedSortedArray.Solution sol = new();
foreach (var item in new (int[] nums, int target)[]{
    ([1,2,3], -1),
    ([1,2,3], 1),
    ([1,2,3], 2),
    ([1,2,3], 3), 
    ([0,1], -1), 
    ([0,1], 0), 
    ([0,1], 1), 
    ([42], 42), 
    ([4,5,6,7,0,1,2], 0),
    ([4,5,6,7,0,1,2], 1),
    ([4,5,6,7,0,1,2], 2),
    ([4,5,6,7,0,1,2], 4),
    ([4,5,6,7,0,1,2], 5),
    ([4,5,6,7,0,1,2], 7), 
    ([4,5,6,7,0,1,2,3], 0), 
    ([4,5,6,7,0], 0)
    })
{
    System.Console.WriteLine($"{item.nums.ToPrintVersion()} - {item.target} : {sol.Search(item.nums, item.target)}");
}



// FortySixPermutations.Solution sol = new();
// foreach (var item in new int[][]{[1,2,3], [0,1], [42]})
// {
//     System.Console.WriteLine($"{item.ToPrintVersion()} : {sol.Permute(item).Select(c => $"[{c.ToPrintVersion()}]").ToPrintVersion()}");
// }

// SeventeenLetterCombinationsOfPhoneNumber.Solution sol = new();

// foreach (var item in new []{"23", "2"})
// {
//     System.Console.WriteLine($"{item} : {string.Join(", ", sol.LetterCombinations(item))}");
// }

// ThreeLongestSubstringWithoutRepeatingCharacters.Solution sol = new();
// foreach (var item in new []{"abcabcbb", "bbbbb", "pwwkew"})
// {
//     System.Console.WriteLine($"{item} : {sol.LengthOfLongestSubstring(item)}");
// }

// EightStringIntAtoi.Solution sol = new();
// foreach (var item in new []{"-91283472332", "   -042", "", "42", "-42", "0002", "+2", "-0002", "1337c0d3"})
// {
//     System.Console.WriteLine($"{item} : {sol.MyAtoi(item)}");
// }



// Console.WriteLine("Hello World!");
// var ints = new[]
// {
//     new[] { 1, 4, 7, 11, 15 },
//     new[] { 2, 5, 8, 12, 19 }, 
//     new[] { 3, 6, 9, 16, 22 },
//     new[] { 10, 13, 14, 17, 24 },
//     new[] { 18, 21, 23, 26, 30 }
// };
// var searchMatrix = new Search2DMatrix2().SearchMatrix(ints, 8);
// Console.WriteLine();
// new RotateImage().Rotate(arr);
// new PascalsTriangle2Solution().GetRow(0).PrintResult();
// new PascalsTriangle2Solution().GetRow(1).PrintResult();
// new PascalsTriangle2Solution().GetRow(2).PrintResult();
// new PascalsTriangle2Solution().GetRow(3).PrintResult();
// var linkedList = new LinkedList<int>();
// var lowestCommonAncestor = new BSTLowestCommonAncestor()
//     .LowestCommonAncestor(
//         new TreeNode(6, new TreeNode(2, new TreeNode(0), new TreeNode(4))),
//         new TreeNode(2),
//         new TreeNode(4));

// var isSymmetric = new BinaryTreeIsSymmetric().IsSymmetric(
//     new BinaryTreeIsSymmetric.TreeNode(1, new BinaryTreeIsSymmetric.TreeNode(2),
//         new BinaryTreeIsSymmetric.TreeNode(2)));


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

