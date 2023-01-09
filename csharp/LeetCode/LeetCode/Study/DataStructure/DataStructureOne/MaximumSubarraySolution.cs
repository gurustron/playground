using System;

namespace LeetCode.Study.DataStructure.DataStructureOne;

/// <summary>
/// https://leetcode.com/problems/maximum-subarray/description/
/// </summary>
public class MaximumSubarraySolution
{
    public int MaxSubArray(int[] nums) 
    {
        int bestSum = int.MinValue;
        int sum = 0;

        for (int i = 0, n = nums.Length; i < n; i++)
        {
            sum = Math.Max(sum + nums[i], nums[i]);
            bestSum = Math.Max(bestSum, sum);
        }

        return bestSum;
    }
    
    public int MySolution(int[] nums)
    {
        var tmpArray = new int[nums.Length - 1]; // max of rolling sum and current
        var rollingSum = 0;
        for (int i = nums.Length - 1; i > 0 ; i--)
        {
            var curr = nums[i];
            rollingSum += curr;
            if (rollingSum < 0)
            {
                rollingSum = 0;
            }

            tmpArray[i - 1] = rollingSum;
        }

        var max = int.MinValue;
        for (int i = 0; i < nums.Length - 1; i++)
        {
            max = Math.Max(max, Math.Max(nums[i], nums[i] + tmpArray[i]));
        }

        return Math.Max(max, nums[^1]);
    }
}