namespace LeetCode
{
    public class TwoSums
    {
        public int[] TwoSum(int[] nums, int target)
        {
            for (var i = 0; i < nums.Length; i++)
            for (var j = i + 1; j < nums.Length; j++)
            {
                var diff = nums[i] + nums[j] - target;
                if (diff == 0) return new[] {i, j};
            }

            return default;
        }
    }
}