using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCode.Study.DataStructure.DataStructureTwo;

public class ThreeSumSolution
{
    public IList<IList<int>> ThreeSum(int[] nums)
    {
        var result = new HashSet<IList<int>>(new ThreeElementsListComparer());
        Array.Sort(nums);
        var counts = nums
            .GroupBy(i => i)
            .ToDictionary(g => g.Key, g => g.Count());

        for (int i = 0; i < nums.Length - 1; i++)
        {
            for (int j = i + 1; j < nums.Length; j++)
            {
                var elI = nums[i];
                var elJ = nums[j];
                var elK = 0 - elI - elJ;
                if (counts.TryGetValue(elK, out var elKCount))
                {
                    if (elJ == elK && elK == elI)
                    {
                        if (elKCount >= 3) result.Add(new List<int>(3) { elI, elJ, elK });
                    }
                    else if (elI == elK || elJ == elK)
                    {
                        if (elKCount >= 2) result.Add(new List<int>(3) { elI, elK, elJ });
                    }
                    else
                    {
                        var item = new List<int>(3) { elI, elK, elJ };
                        item.Sort();
                        result.Add(item);
                    }
                }
            }
        }

        return result.ToList();
    }

    class ThreeElementsListComparer : IEqualityComparer<IList<int>>
    {
        public bool Equals(IList<int> x, IList<int> y) =>
            x.Count == 3
            && y.Count == 3
            && x.SequenceEqual(y);

        public int GetHashCode(IList<int> obj) => HashCode.Combine(obj[0], obj[1], obj[2]);
    }
}

