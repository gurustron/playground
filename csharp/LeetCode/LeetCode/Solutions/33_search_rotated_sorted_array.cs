namespace ThirtyThreeSearchInRotatedSortedArray;

public class Solution 
{
    public int Search(int[] nums, int target) 
    {
        if(nums.Length == 1)
        {
            return nums[0] == target ? 0 : -1;
        }

        // find the actual sorted start
        int actualStart = 0;

        if(nums[0] > nums[nums.Length - 1])
        {
            int DivideAndConquerTheRotated(int left, int right)
            {
                if(right - left <= 1)
                {
                    return CheckIsTheActualSortedStart(right) ? right : left; // ???
                }

                var middle = (left + right) / 2;

                if(CheckIsTheActualSortedStart(middle)) return middle;

                if(nums[middle] > nums[right]) return DivideAndConquerTheRotated(middle, right);

                return DivideAndConquerTheRotated(left, middle);

            }

            bool CheckIsTheActualSortedStart(int index)
            {
                if(index == nums.Length - 1 && nums[index - 1] > nums[index]) return true;
                if(index == 0 && nums[index] > nums[index + 1]) return true;

                if(nums[index-1] > nums[index] && nums[index] < nums[index + 1]) return true;

                return false;
            }

            actualStart = DivideAndConquerTheRotated(0, nums.Length - 1);

            if(nums[actualStart] <= target && nums[nums.Length - 1] >= target) return TargetDivideAndConquer(actualStart, nums.Length - 1);

            if(nums[0] <= target && nums[actualStart - 1] >= target) return TargetDivideAndConquer(0, actualStart - 1);

            return -1;
        }
        else
        {
            return TargetDivideAndConquer(0, nums.Length - 1);
        }
        
        return actualStart;

        int TargetDivideAndConquer(int left, int right)
        {
            if(nums[left] == target) return left;
            if(nums[right] == target) return right;
            if(right - left <= 1) return -1;
            if(nums[left] > target || nums[right] < target) return -1;


            var middle = (left + right) /2;

            if(nums[middle] == target) return middle;

            if(nums[middle] > target) return TargetDivideAndConquer(left, middle);
            
            return TargetDivideAndConquer(middle, right); 
        }
    }
}