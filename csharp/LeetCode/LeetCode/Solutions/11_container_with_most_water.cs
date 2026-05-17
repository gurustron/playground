namespace ElevenContainerWithMostWater;

using System;

public class Solution
{
    public int MaxArea(int[] height)
    {
        int left = 0;
        int right = height.Length - 1;
        int result = 0;

        while(left < right)
        {
            result = Math.Max((right - left) * Math.Min(height[right], height[left]), result);

            if(height[right] > height[left])
            {
                left++;
            }
            else
            {
                right--;
            }
        }

        return result;
    }
}