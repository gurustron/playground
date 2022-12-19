using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCode.Study.DataStructure.DataStructureOne;

/// <summary>
/// https://leetcode.com/problems/maximum-depth-of-binary-tree
/// </summary>
public class BinaryTreeMaximumDepth
{

    public int MaxDepth(TreeNode root)
    {
        var result = 0;
        if (root is null)
        {
            return result;
        }

        var treeNodes = new Queue<TreeNode>();
        treeNodes.Enqueue(root);

        while (treeNodes.Count > 0)
        {
            var levelCount = treeNodes.Count;
            result++;
            for (int i = 0; i < levelCount; i++)
            {
                var treeNode = treeNodes.Dequeue();
                if (treeNode.left is not null)
                {
                    treeNodes.Enqueue(treeNode.left);
                }

                if (treeNode.right is not null)
                {
                    treeNodes.Enqueue(treeNode.right);
                }
            }
        }

        return result;
    }

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