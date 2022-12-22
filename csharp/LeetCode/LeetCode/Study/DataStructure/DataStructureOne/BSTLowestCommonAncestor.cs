using System;
using System.Collections.Generic;

namespace LeetCode.Study.DataStructure.DataStructureOne;

public class BSTLowestCommonAncestor
{
    public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
    {
        var node = root;
        var minVal = Math.Min(p.val, q.val);
        var maxVal =  Math.Max(p.val, q.val);
        while (true)
        {
            var minIsCurrOrLeft = minVal <= node.val;
            var maxIsCurrOrRight = maxVal >= node.val;
            if (minIsCurrOrLeft && maxIsCurrOrRight)
            {
                return node;
            }
            
            if (minVal < node.val && maxVal < node.val)
            {
                node = node.left ?? throw new InvalidOperationException($"{minVal} - {maxVal} - {node.val} both Less and null left");
                continue;
            }

            if (maxVal > node.val && minVal > node.val)
            {
                node = node.right ?? throw new InvalidOperationException($"{minVal} - {maxVal} - {node.val} both Greater and null left");
                continue;
            }

            throw new InvalidOperationException($"Should not be here {minVal} - {maxVal} - {node.val}");
        }
    }


}