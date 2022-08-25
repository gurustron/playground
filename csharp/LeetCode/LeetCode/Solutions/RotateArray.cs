using System;

namespace LeetCode
{
    public class RotateArray
    {
        public void Rotate(int[] nums, int k)
        {
            // k = k % nums.Length;
            // var counter = 0;
            // var index = 0;
            // var mem = nums[index];
            // while (counter < nums.Length)
            // {
            //     index = (index + k) % nums.Length;
            //     var curr = nums[index];
            //     nums[index] = mem;
            //     mem = curr;
            //     counter++;
            //     Console.WriteLine(string.Join(',', nums));
            // }

            var length = nums.Length;
            var copy = new int[length];
            for (var i = 0; i < length; i++) copy[(i + k) % length] = nums[i];
            Array.Copy(copy, nums, length);
        }
    }
}