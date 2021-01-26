using System;
using System.Collections.Generic;
using System.Linq;

namespace FintechExam
{
    public class Task7
    {
        public static void Run()
        {
            var max = long.Parse(Console.ReadLine());
            var list = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();
            Console.WriteLine(Run(max, list));
        }

        private static long Run(long max, List<int> l)
        {
            long result = 0;
            l = l.OrderByDescending(i => i).ToList();
            var n1 = l[0];
            var n2 = l[1];
            var n3 = l[2];
            max = max - 1;
            for (int i = 0; n1 * i <= max; i++)
            {
                if (i != 0) result += 1;
                var maxI = max - n1 * i;
                for (int j = 0; j * n2 <= maxI; j++)
                {
                    if (j != 0) result += 1;
                    var k = (maxI - j * n2) / n3;
                    result += k;
                }
            }

            return result + 1;
        }

    }
}