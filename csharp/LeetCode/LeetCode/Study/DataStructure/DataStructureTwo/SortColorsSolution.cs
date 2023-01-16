using System;

namespace LeetCode.Study.DataStructure.DataStructureTwo;

public class SortColorsSolution
{
    // One pass
    public void SortColors(int[] nums)
    {
        int left = 0;
        int right = nums.Length;
        int countW = 0;
        while (left < right)
        {
            var numLeft = nums[left];
            if (numLeft == 0)
            {
                left++;
                continue;
            }
            if (numLeft == 1)
            {
                left++;
                countW++;
                
                continue;
            }
            
            
            
            
        }
    }
    public void SortColorsEasy(int[] nums)
    {
        var countR = 0;
        var countW = 0;
        var countB = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            switch (nums[i])
            {
                case 0:
                    countR++;
                    break;
                case 1:
                    countW++;
                    break;
                case 2:
                    countB++;
                    break;
            }
        }

        for (int i = 0; i < nums.Length; i++)
        {
            if (i < countR) nums[i] = 0;
            else if (i < countR + countW) nums[i] = 1;
            else nums[i] = 2;
        }
    }
}
