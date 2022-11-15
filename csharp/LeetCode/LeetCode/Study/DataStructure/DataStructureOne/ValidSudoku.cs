using System;
using System.Collections.Generic;

namespace LeetCode.Study.DataStructure.DataStructureOne;

public class ValidSudoku
{
    public bool IsValidSudoku(char[][] board) 
    {
        var hashSet = new HashSet<string>();
        Span<char> span = stackalloc char[4];

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                var val = board[i][j];
                if (val == '.')
                {
                    continue;
                }


                span[3] = val;
                
                // row
                span[0] = 'r';
                span[1] = Convert.ToChar(i);
                span[2] = '-';
                if (!hashSet.Add(new string(span)))
                {
                    return false;
                }
                
                // column
                span[0] = 'c';
                span[1] = Convert.ToChar(j);
                span[2] = '-';
                if (!hashSet.Add(new string(span)))
                {
                    return false;
                }
                
                // block
                span[0] = 'b';
                span[1] = Convert.ToChar(i / 3);
                span[2] = Convert.ToChar(j / 3);
                if (!hashSet.Add(new string(span)))
                {
                    return false;
                }
            }
        }

        return true;
    }
}

