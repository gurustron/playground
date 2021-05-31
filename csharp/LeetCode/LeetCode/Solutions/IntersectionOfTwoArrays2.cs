using System.Collections.Generic;
using System.Linq;

namespace LeetCode
{
    public class IntersectionOfTwoArrays2
    {
        public int[] Intersect(int[] nums1, int[] nums2)
        {
            if (nums2.Length > nums1.Length)
            {
                return Intersect(nums2, nums1);
            }

            var dictionary = nums2
                .GroupBy(i => i)
                .ToDictionary(g => g.Key, g => g.Count());
            var result = new List<int>(nums1.Length);
            foreach (var n in nums1)
            {
                if (dictionary.ContainsKey(n) && dictionary[n] > 0)
                {
                    result.Add(n);
                    dictionary[n] -= 1;
                }
            }

            return result.ToArray();
        }
    }
}