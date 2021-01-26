using System;
using System.Collections.Generic;
using System.Linq;

namespace FintechExam
{
    public class Task4
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
                .Select(int.Parse)
                .ToList();

            Console.WriteLine(Run(n, numbers));
        }

        public static string Run(int i, List<int> numbers)
        {
            var withoutPresents = new HashSet<int>(Enumerable.Range(1, numbers.Count));
            for (int index = 0; index < numbers.Count; index++)
            {
                withoutPresents.Remove(numbers[index]);
            }

            if (withoutPresents.Count > 1 || withoutPresents.Count == 0)
            {
                return "-1 -1";
            }

            var keyWithoutPresent = withoutPresents.First();
            var list = numbers
                .Select((n, index) => new {n, i = index + 1})
                .GroupBy(i1 => i1.n)
                .First(g => g.Count() > 1)
                .ToList();
            if (list.Count != 2)
            {
                return "-1 -1";
            }

            foreach (var g in list)
            {
                var edges = numbers
                    .Select((n, i) => new {n, i = i + 1})
                    .ToDictionary(arg => arg.i, arg => arg.n);
                edges[g.i] = keyWithoutPresent;

                var startNodes = new HashSet<int>(edges.Keys);
                var cycle = new HashSet<int>();
                var currNode = 1;
                while (!cycle.Contains(currNode))
                {
                    startNodes.Remove(currNode);
                    cycle.Add(currNode);
                    currNode = edges[currNode];
                }

                if (!startNodes.Any())
                {
                    return g.i + " " + keyWithoutPresent;
                }
            }

            return "-1 -1";
        }
    }
}