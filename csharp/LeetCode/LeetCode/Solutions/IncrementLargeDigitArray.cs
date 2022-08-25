using System;

namespace LeetCode
{
    public class IncrementLargeDigitArray
    {
        public class Solution
        {
            public int[] PlusOne(int[] digits)
            {
                bool increment = true;
                int index = digits.Length - 1;
                do
                {
                    var curr = digits[index];
                    increment = curr + 1 > 9;
                    digits[index] = (curr + 1) % 10;
                    index--;
                } while (increment && index >= 0);

                if (increment)
                {
                    var res = new int[digits.Length + 1];
                    res[0] = 1;
                    Array.Copy(digits,0, res, 1, digits.Length);
                    return res;
                }
                else
                {
                    return digits;
                }
            }
        }
    }
}

