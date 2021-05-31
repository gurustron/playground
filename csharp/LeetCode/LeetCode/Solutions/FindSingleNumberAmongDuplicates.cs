namespace LeetCode
{
    public class FindSingleNumberAmongDuplicates
    {
        public int SingleNumber(int[] nums)
        {
            var a = 0;
            for (var i = 0; i < nums.Length; i++)
            {
                a ^= nums[i];
            }

            return a;
        }

        // public int SingleNumber(int[] nums)
        // {
        //     var bitArray = new BitArray(60001);
        //     var plus = 30000;
        //     foreach (var num in nums)
        //     {
        //         var curr = num + plus;
        //         bitArray[curr] = !bitArray[curr];
        //     }
        //
        //     foreach (var num in nums)
        //     {
        //         if (bitArray[num + plus])
        //         {
        //             return num;
        //         }
        //     }
        //
        //     throw new Exception("Wrong");
        // }
    }
}