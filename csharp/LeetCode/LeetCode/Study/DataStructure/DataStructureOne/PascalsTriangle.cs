using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCode.Study.DataStructure.DataStructureOne;

public class PascalsTriangle
{
    public IList<IList<int>> Generate(int numRows) 
    {
        var result = new List<IList<int>>(numRows)
        {
            new List<int>{1}
        };
       
        var len = 1;
        for (int i = 1; i < numRows; i++)
        {
            len += 1;
            var row = Enumerable.Repeat(0, len).ToList();
            result.Add(row);
            var prevRow = result[i - 1];

            for (int j = 0; j < len; j++)
            {
                if (j - 1 >= 0)
                {
                    row[j] += prevRow[j - 1];
                }
                if(j < prevRow.Count)
                {
                    row[j] += prevRow[j];
                }
            }
        }
        return result;
    }
}