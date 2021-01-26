using System;
using System.Linq;

namespace FintechExam
{
    public class SecondTask
    {

        public static void Run()
        {
            Console.WriteLine(Run(Console.ReadLine()));
        }

        public static long Run(string s)
        {
            var nk = s.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(s => new {Num = long.Parse(s),FirstDigit= s.First()})
                .ToList();
            var l = nk[0];
            var r = nk[1];
            var expL = CountExp(l.Num);
            var expR = CountExp(r.Num);
            long result = expR > expL
                ? expR - expL - 1
                : 0;
            result *= 9;

            var l1 = long.Parse(new string(l.FirstDigit, expL + 1));
            var r1 = long.Parse(new string(r.FirstDigit, expR + 1));
            int lDigit = (int) char.GetNumericValue(l.FirstDigit);
            var rDigit = (int) char.GetNumericValue(r.FirstDigit);
            if (expR == expL)
            {
                result += rDigit - lDigit - 1;
                if (l1 >= l.Num) result++;
                if (r.Num >= r1) result++;
            }
            else
            {
                result += l1 >= l.Num
                    ? 9L - lDigit + 1L
                    : 9 - lDigit;

                result += r.Num >= r1
                    ? rDigit
                    : rDigit - 1;
            }
            return result;
        }

        public static int CountExp(long num)
        {
            int result = 0;
            while (num/10 > 0)
            {
                result++;
                num = num / 10;
            }

            return result;
        }
    }
}