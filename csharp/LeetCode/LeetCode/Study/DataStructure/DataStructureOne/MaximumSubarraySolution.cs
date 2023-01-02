using System;

namespace LeetCode.Study.DataStructure.DataStructureOne;

/// <summary>
/// https://leetcode.com/problems/maximum-subarray/description/
/// </summary>
public class MaximumSubarraySolution
{
    public int MaxSubArray(int[] nums)
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