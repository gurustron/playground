namespace ThirtyFourFindFirstAndLastPositionAfElementInASortedArray;


public class Solution
{
    private static int[] NotFound = [-1, -1];
    public int[] SearchRange(int[] nums, int target)
    {
        if (nums.Length == 0) return NotFound;
        
        int FindNumberPostion(int left, int right)
        {           
            if(nums[right] == target) return right;

            if(nums[left] == target) return left;
            
            if(right - left <= 1)
            {
                return -1;
            }

            var middle = (left + right)/2;
            var currNum = nums[middle];
            if(currNum == target) return middle;

            if(currNum < target) return FindNumberPostion(middle, right);

            return FindNumberPostion(left, middle); 
        }

        var anyNumberPostition = FindNumberPostion(0, nums.Length - 1);

        if(anyNumberPostition == -1) return NotFound;
        System.Console.WriteLine(anyNumberPostition);
        int FindLeftBorder(int from, int to)
        {
            if(from == 0 && nums[from] == target) return from;
            if(to - from <= 1) return nums[from] == target ? from : to;

            var middle = (from + to) / 2;

            if(nums[middle] == target) return FindLeftBorder(from, middle);

            return FindLeftBorder(middle, to);
        }

        int FindRightBorder(int from, int to)
        {
            if(to == nums.Length - 1 && nums[to] == target) return to;
            if(to - from <= 1) return nums[to] == target ? to : from;

            var middle = (from + to) / 2;

            if(nums[middle] == target) return FindRightBorder(middle, to);

            return FindRightBorder(from, middle);
        }

        return [FindLeftBorder(0, anyNumberPostition), FindRightBorder(anyNumberPostition, nums.Length - 1)];
    }
}