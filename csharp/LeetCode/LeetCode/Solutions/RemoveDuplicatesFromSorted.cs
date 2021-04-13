using System.Linq;

namespace LeetCode
{
    // https://leetcode.com/explore/interview/card/top-interview-questions-easy/92/array/727/
    public class RemoveDuplicatesFromSorted
    {
        public int RemoveDuplicates(int[] nums)
        {
            if (nums.Length == 0) return 0;

            var currentValue = nums[0];
            var nextFreeIndex = 1;
            for (int i = 1; i < nums.Length; i++)
            {
                if (currentValue != nums[i])
                {
                    currentValue = nums[i];
                    nums[nextFreeIndex] = currentValue;
                    nextFreeIndex++;
                }
            }

            return nextFreeIndex;
        }
    }
}