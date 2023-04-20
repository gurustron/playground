namespace LeetCode.Study.DataStructure.DataStructureTwo;

public class RotateImage
{
    public void Rotate(int[][] matrix)
    {
        var n = matrix.Length;
        for (int i = 0, j = n-1; i < j; i++, j--)
        {
            if (i + 1 == j)
            {
                var next = matrix[i][j];
                matrix[i][j] = matrix[i][i];
                (next, matrix[j][j]) = (matrix[j][j], next);
                (next, matrix[j][i]) = (matrix[j][i], next);
                matrix[i][i] = next;
                continue;
            }
            for (var index = i; index < j; index++)
            {
                var next = matrix[index][j];
                matrix[index][j] = matrix[i][index]; // 1
                (next, matrix[j][j - index]) = (matrix[j][j - index], next); // 2
                (next, matrix[j-index][i]) = (matrix[j-index][i], next); // 3
                matrix[i][index] = next; // 4
            }
        }
    }
}

// next = [1][2]  8
// [2][1]