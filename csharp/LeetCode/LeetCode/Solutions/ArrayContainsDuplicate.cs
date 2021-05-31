using System.Collections.Generic;

namespace LeetCode
{
    public class ArrayContainsDuplicate
    {
            public bool ContainsDuplicate(int[] nums)
            {
                var hashSet = new HashSet<int>(nums.Length);
                for (int i = 0; i < nums.Length; i++)
                {
                    hashSet.Add(nums[i]);
                    if (hashSet.Count != i + 1)
                    {
                        break;
                    }
                }
                return hashSet.Count != nums.Length;
            }
    }
}