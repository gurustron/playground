using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCode.Study.DataStructure.DataStructureOne;

/// <summary>
/// https://leetcode.com/problems/maximum-depth-of-binary-tree
/// </summary>
public class BinaryTreeIsSymmetric
{
    public bool IsSymmetric(TreeNode root) 
    {
        if (root.left is null && root.right is null)
        {
            return true;
        }
    
        if (root.left is null || root.right is null)
        {
            return false;
        }
        var queue = new Queue<TreeNode>();
        queue.Enqueue(root.left);
        queue.Enqueue(root.right);

        while (queue.Count > 0)
        {
            var left = queue.Dequeue();
            var right = queue.Dequeue();
            if (left.val != right.val)
            {
                return false;
            }

            if (left.left is not null && right.right is not null)
            {
                queue.Enqueue(left.left);
                queue.Enqueue(right.right);
            }
            else if(left.left is not null || right.right is not null)
            {
                return false;
            }
            
            if (left.right is not null && right.left is not null)
            {
                queue.Enqueue(left.right);
                queue.Enqueue(right.left);
            }
            else if(left.right is not null || right.left is not null)
            {
                return false;
            }
        }

        return true;
    }
    
    // public bool IsSymmetric(TreeNode root) 
    // {
    //     if (root.left is null && root.right is null)
    //     {
    //         return true;
    //     }
    //
    //     var levelCount = 2;
    //     var treeNodes = new TreeNode[]
    //     {
    //         root.left,
    //         root.right
    //     };
    //
    //     while (true)
    //     {
    //         levelCount *= 2;
    //         var newTreeNodes = new TreeNode[levelCount];
    //         for (int i = 0; i < treeNodes.Length/2; i++)
    //         {
    //             var left = treeNodes[i];
    //             var right = treeNodes[^(i + 1)];
    //             if (!AreMirrored(left, right))
    //             {
    //                 return false;
    //             }
    //             var i2 = i * 2;
    //             newTreeNodes[i2] = left?.left;
    //             newTreeNodes[i2 + 1] = left?.right;
    //             newTreeNodes[^(i2 + 1)] = right?.right;
    //             newTreeNodes[^(i2 + 2)] = right?.left;
    //         }
    //
    //         if (newTreeNodes.Any(n => n is not null))
    //         {
    //             treeNodes = newTreeNodes;
    //         }
    //         else
    //         {
    //             break;
    //         }
    //     }
    //
    //     return true;
    //     bool AreMirrored(TreeNode left, TreeNode right)
    //     {
    //         if (left is null && right is null)
    //         {
    //             return true;
    //         }
    //
    //         if (left is null || right is null)
    //         {
    //             return false;
    //         }
    //
    //         return left.val == right.val;
    //     }
    // }

    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;

        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }
}