using System;

namespace LeetCode.Study.DataStructure.DataStructureTwo;

public class Search2DMatrix2
{
    public bool SearchMatrix(int[][] matrix, int target)
    {
        int m = matrix.Length;
        int n = matrix[0].Length;
        // return SearchMatrix(matrix, 0, 0, m, n, target);
        
        return SearchMatrix(matrix, 0, 0, m-1, n-1, target);
    }

    private bool SearchMatrix(int[][] matrix, int fromM, int fromN, int toM, int toN, int target)
    {
        // hack
        if (fromM >= matrix.Length || fromN >= matrix[0].Length)
        {
            return false;
        }
        
        var curr = matrix[fromM][fromN];
        if (curr > target)
        {
            return false;
        }
        
        if (curr == target)
        {
            return true;
        }

        var currM = fromM; 
        var currN = fromN;
        
        while (currM <= toM || currN <= toN)
        {
            currM++;
            currN++;

            if (currM > toM && currN > toN)
            {
                break;
            }
            
            if (currM > toM)
            {
                currM = fromM;
            }

            if (currN > toN)
            {
                currN = fromN;
            }
            
            curr = matrix[currM][currN];
            if (curr == target)
            {
                return true;
            }

            if (curr > target)
            {
                if (SearchMatrix(matrix, fromM, currN, currM - 1, toN, target)
                    || SearchMatrix(matrix, currM, fromN, toM, currN - 1, target))
                {
                    return true;
                }

                break;
            }
        }

        return false;
    }

    // private bool SearchMatrix(int[][] matrix,
    //     int fromM,
    //     int fromN,
    //     int toM,
    //     int toN,
    //     int target)
    // {
    //     var l = matrix[fromM][fromN];
    //     if (l > target)
    //     {
    //         return false;
    //     }
    //
    //     if (l == target)
    //     {
    //         return true;
    //     }
    //
    //     // for diag in diags
    //     var sideM = toM - fromM;
    //     var sideN = toN - fromN;
    //     var step = Math.Min(sideM, sideN);
    //     var limit = Math.Max(sideM, sideN);
    //     for (int diag = 0; diag + step <= limit; diag += step)
    //     {
    //         for (int i = 0; i <= step; i++)
    //         {
    //             var curr = matrix[fromM + i][fromN + i];
    //             if (curr == target)
    //             {
    //                 return true;
    //             }
    //
    //             if (curr > target)
    //             {
    //                 if (SearchMatrix(matrix, fromM, fromN + i, fromM + i - 1, toN, target)
    //                     || SearchMatrix(matrix, fromM + i, fromN, toM, fromN + i - 1, target))
    //                 {
    //                     return true;
    //                 }
    //
    //                 break;
    //             }
    //         }
    //
    //         if (sideM > sideN)
    //         {
    //             fromM = fromM + step;
    //         }
    //         else
    //         {
    //             fromN = fromN + step;
    //         }
    //     }
    //
    //     return false;
    // }
}