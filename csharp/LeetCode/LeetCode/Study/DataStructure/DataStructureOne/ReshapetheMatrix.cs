namespace LeetCode.Study.DataStructure.DataStructureOne;

public class ReshapetheMatrix
{
    public int[][] MatrixReshape(int[][] mat, int r, int c) 
    {
        int rowsOrHeight = mat.Length;
        int colsOrWidth = mat[0].Length;
        
        if(rowsOrHeight * colsOrWidth != r * c)
        {
            return mat;
        }

        int counter = 0;
        int[][] result = new int[r][];
        for(int i = 0; i < r; i++)
        {
            var row = new int[c];
            result[i] = row;
            for (int j = 0; j < c; j++)
            {
                row[j] = mat[counter/colsOrWidth][counter%colsOrWidth];
                counter++;
            }
        }

        return result;
    }
}