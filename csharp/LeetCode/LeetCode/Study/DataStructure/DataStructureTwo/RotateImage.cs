namespace LeetCode.Study.DataStructure.DataStructureTwo;

/// <summary>
/// https://leetcode.com/problems/rotate-image
/// </summary>
public class RotateImage
{
    public void Rotate(int[][] matrix)
    {
        var n = matrix.Length;
        for (int i = 0, j = n-1; i < j; i++, j--)
        {
            // if (i + 1 == j)
            // {
            //     var next = matrix[i][j];
            //     matrix[i][j] = matrix[i][i];
            //     (next, matrix[j][j]) = (matrix[j][j], next);
            //     (next, matrix[j][i]) = (matrix[j][i], next);
            //     matrix[i][i] = next;
            //     continue;
            // }
            for (int index = i, indexJ = j; index < j; index++, indexJ--)
            {
                var next = matrix[index][j];
                matrix[index][j] = matrix[i][index]; // 1
                (next, matrix[j][indexJ]) = (matrix[j][indexJ], next); // 2
                (next, matrix[indexJ][i]) = (matrix[indexJ][i], next); // 3
                matrix[i][index] = next; // 4
            }
        }
    }
}
