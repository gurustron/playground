namespace LeetCode.Study.DataStructure.DataStructureOne;

public class MergeSortedArrays
{
    public void Merge(int[] nums1, int m, int[] nums2, int n)
    {
        int left = 0;
        int right = 0;
        int resultIndex = 0;
        int[] result = new int[m + n];
        while (left < m && right < n)
        {
            result[resultIndex++] = nums1[left] < nums2[right]
                ? nums1[left++]
                : nums2[right++];
        }

        for (; left < m; left++)
        {
            result[resultIndex++] = nums1[left];
        }

        for (; right < n; right++)
        {
            result[resultIndex++] = nums2[right];
        }

        for (int i = 0; i < nums1.Length; i++)
        {
            nums1[i] = result[i];
        }
    }
}