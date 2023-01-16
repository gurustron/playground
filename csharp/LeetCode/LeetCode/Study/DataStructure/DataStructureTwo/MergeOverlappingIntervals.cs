using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCode.Study.DataStructure.DataStructureTwo;

public class MergeOverlappingIntervals
{
    public int[][] Merge(int[][] intervals) => intervals
        .OrderBy(i => i[0])
        .Aggregate(
            new LinkedList<int[]>(),
            (aggr, t) =>
                aggr switch
                {
                    // { Count: 0 } => aggr.AddLast(t).List,
                    { Last:{} node } when node.Value[1] <= t[1] => UpdateLast(aggr, ints => ints[1] = Math.Max(ints[1], t[1])),
                    _ => aggr.AddLast(t).List
                }
        )
        .ToArray();

    private static LinkedList<int[]> UpdateLast(LinkedList<int[]> l, Action<int[]> action)
    {
        action(l.Last.Value);
        return l;
    }
}