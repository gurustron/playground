using System;
using System.Collections.Generic;

namespace LeetCode.Study.DataStructure.DataStructureOne;

/// <summary>
/// https://leetcode.com/problems/binary-tree-level-order-traversal
/// </summary>
public class BinaryTreeLevelOrderTraversal
{
    public IList<IList<int>> LevelOrder(TreeNode root) 
    {
        var result = new List<IList<int>>();
        if (root is null)
        {
            return result;
        }

        var treeNodes = new Queue<(int Level, TreeNode Node)>();
        int currentLevel = 0;
        var currentLevelValues = new List<int>();
        result.Add(currentLevelValues);
        treeNodes.Enqueue((currentLevel, root));

        while (treeNodes.TryDequeue(out var tuple))
        {
            var (level, node) = tuple;

            if (level < currentLevel)
            {
                throw new Exception("SomethingWrong");
            }

            if (level > currentLevel)
            {
                currentLevel = level;
                currentLevelValues = new List<int>();
                result.Add(currentLevelValues);
            }

            currentLevelValues.Add(node.val);

            if (node.left is not null)
            {
                treeNodes.Enqueue((level + 1, node.left));
            }

            if (node.right is not null)
            {
                treeNodes.Enqueue((level + 1, node.right));
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