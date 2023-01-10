using System.Linq;

namespace LeetCode.Study.DataStructure.DataStructureTwo;

public class MajorityElementSolution
{
    public int MajorityElement(int[] nums) => nums.ToLookup(i => i)
            .MaxBy(g => g.Count())
            .Key;
}