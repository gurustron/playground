namespace FortySixPermutations;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;

public class Solution 
{
    public IList<IList<int>> Permute(int[] nums) 
    {
        List<IList<int>> result = new(Enumerable.Range(1, nums.Length).Aggregate((x, y) => x*y));

        void Solve(HashSet<int> remainder, IList<int> agg)
        {
            if(remainder.Count == 0)
            {
                result.Add(agg);
            }

            foreach(var i in remainder)
            {
                var copy = remainder.ToHashSet();
                copy.Remove(i);
                var aggCopy = agg.ToList();
                aggCopy.Add(i);
                Solve(copy, aggCopy);
            }
        }

        Solve(new(nums), new List<int>(nums.Length));

        return result;
    }
}