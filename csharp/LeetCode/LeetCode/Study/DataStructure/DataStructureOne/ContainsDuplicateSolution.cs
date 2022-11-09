using System.Collections.Generic;

namespace LeetCode.Study.DataStructure.DataStructureOne;

public class ContainsDuplicateSolution
{
    public bool ContainsDuplicate(int[] nums) 
    {
        var hashSet = new HashSet<int>();
        for (var index = 0; index < nums.Length; index++)
        {
            // if (!hashSet.Add(nums[index]))
            //     return true;
            var num = nums[index];
            if (hashSet.Contains(num))
            {
                return true;
            }
            hashSet.Add(num);
        }

        return false;
    }
}