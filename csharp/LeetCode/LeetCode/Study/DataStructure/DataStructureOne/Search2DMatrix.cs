namespace LeetCode.Study.DataStructure.DataStructureOne;

public class Search2DMatrix
{
    public bool SearchMatrix(int[][] matrix, int target)
    {
        var m = matrix.Length - 1;
        var n = matrix[0].Length-1;

        var left = 0;
        var right = m;
        while (right - left > 1)
        {
            var mid = (left + right) / 2;
            var midValue = matrix[mid][0];
            if(midValue == target)
            {
                return true;
            }
            
            if (midValue < target)
            {
                left = mid;
            }
            else
            {
                right = mid;
            }
        }
        var rightValue = matrix[right][0];

        var arr = target < rightValue
            ? matrix[left]
            : matrix[right];

        left = 0;
        right = arr.Length - 1;
        while (right - left > 1)
        {
            var mid = (left + right) / 2;
            var midValue = arr[mid];
            if(midValue == target)
            {
                return true;
            }
            
            if (midValue < target)
            {
                left = mid;
            }
            else
            {
                right = mid;
            }
        }

        return arr[left] == target || arr[right] == target;
    }
}