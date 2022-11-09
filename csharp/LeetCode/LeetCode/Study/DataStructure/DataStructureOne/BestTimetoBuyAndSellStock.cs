namespace LeetCode.Study.DataStructure.DataStructureOne;

public class BestTimetoBuyAndSellStock
{
    public int MaxProfit(int[] prices)
    {
        var reverseMaxes = new int[prices.Length - 1];
        var max = int.MinValue;
        for (int i = prices.Length - 1; i >= 1; i--)
        {
            var curr = prices[i];
            if (curr > max)
            {
                max = curr;
            }

            reverseMaxes[i-1] = max;
        }

        var maxProfit = int.MinValue;
        for (int i = 0; i < prices.Length - 1; i++)
        {
            var curr = prices[i];
            var currMax = reverseMaxes[i];
            var currProfit = currMax - curr;
            if (currProfit > maxProfit)
            {
                maxProfit = currProfit;
            }
        }

        return maxProfit > 0 ? maxProfit : 0;
    }
}