namespace LeetCode.Study.DataStructure.DataStructureTwo;

/// <summary>
/// https://leetcode.com/problems/single-number/
/// </summary>
public class SingleNumberSolution
{
    public int SingleNumber(int[] nums)
    {
        var result = 0;
        foreach (var num in nums)
        {
            result ^= num;
        }

        return result;
    }
}