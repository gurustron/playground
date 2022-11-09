namespace LeetCode.Study.DataStructure.DataStructureOne;

public class MaximumSubarraySolution
{
    public int MaxSubArray(int[] nums)
    {
        var max = int.MinValue;
        
        for (int i = 0; i < nums.Length; i++)
        {
            var maxI = nums[i];
            var sum = maxI;
            for (int j = i + 1; j < nums.Length; j++)
            {
                sum += nums[j];
                if (sum > maxI)
                {
                    maxI = sum;
                }
            }
            
            if(maxI > max)
            {
                max = maxI;
            }
        }
        
        return max;
    }
}