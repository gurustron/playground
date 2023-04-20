using System.Collections.Generic;
using System.Linq;

namespace LeetCode.Study.DataStructure.DataStructureTwo;

public class PascalsTriangle2Solution
{
    public IList<int> GetRow(int rowIndex)
    {
        var curr = new int[rowIndex + 1];
        var next = new int[rowIndex + 1];
        curr[0] = 1;
        next[0] = 1;

        for (int i = 1; i <= rowIndex; i++)
        {
            for (int j = 0; j <= i; j++)
            {
                var left = j == 0 ? 0 : curr[j-1];
                var right = curr[j];
                next[j] = left + right;
            }
            
            (curr, next) = (next, curr);
        }
        return curr;
    }
}