using System;
using System.Collections.Generic;
using System.Linq;

namespace FintechExam
{
    public class ThirdTask
    {
        public static void Run()
        {
            var nk = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();
            var n = nk[0];
            var numbers = Console.ReadLine()
                .Split(" ")
                .Select(long.Parse)
                .ToList();

            Console.WriteLine(Run(n, numbers));
        }

        public static string Run(int i, List<long> numbers)
        {
            var evenCount = numbers.Count(l => l % 2 == 0);
            var oddCount = numbers.Count - evenCount;
            if (oddCount - evenCount > 1 || evenCount > oddCount)
            {
                return "-1 -1";
            }

            int oddNotInPlace = 0;
            int evenNotInPlace = 0;
            int oddIndexToMove = -1;
            int evenIndexToMove = -1;
            for (var index = 0; index < numbers.Count; index++)
            {
                if (index % 2 == 0 && numbers[index] %2 == 0)  // should be odd
                {
                    evenNotInPlace++;
                    evenIndexToMove = index;
                }
                else if(index % 2 != 0 && numbers[index] %2 != 0)
                {
                    oddNotInPlace++;
                    oddIndexToMove = index;
                }
            }

            if (evenNotInPlace != 1 && oddNotInPlace != 1)
            {
                return "-1 -1";
            }

            return string.Join(" ", new [] {oddIndexToMove, evenIndexToMove}.Select(num => num+1).OrderBy(num => num));
        }
    }
}