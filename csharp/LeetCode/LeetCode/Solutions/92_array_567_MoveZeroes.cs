namespace LeetCode
{
    public class MoveZeroes
    {
        public class Solution
        {
            public void MoveZeroes(int[] nums)
            {
                int nextNonZero = 0;
                for (int i = 0; i < nums.Length; i++)
                {
                    var curr = nums[i];
                    if (curr != 0)
                    {
                        nums[nextNonZero] = curr;
                        nextNonZero++;
                    }
                }

                for (int i = nextNonZero; i < nums.Length; i++)
                {
                    nums[i] = 0;
                }
            }
        }
    }
}
