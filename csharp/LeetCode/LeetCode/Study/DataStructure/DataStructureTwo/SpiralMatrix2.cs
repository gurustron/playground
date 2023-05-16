using System;
using System.Collections.Generic;

namespace LeetCode.Study.DataStructure.DataStructureTwo;

public class SpiralMatrix2
{
    public int[][] GenerateMatrix(int n)
    {
        var result = new int[n][];
        for (int i = 0; i < n; i++)
        {
            result[i] = new int[n];
        }

        int counter = 1;
        for (int diagI = 0, j = n-1; diagI <= j; diagI++, j--)
        {
            var diff = j - diagI;
            for (int i = 0; i <= j; i++)
            {
                var @base = counter + i;
                result[diagI][diagI + i] = @base;
                result[diagI + i][j] = @base + diff;
                result[j][j - i] = @base + 2 * diff;
                result[j - i][diagI] = @base + 3 * diff;
            }

            counter += diff * 4 + 1;
        }

        return result;
    }
}