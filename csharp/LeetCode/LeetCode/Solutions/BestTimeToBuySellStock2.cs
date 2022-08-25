namespace LeetCode
{
    public class BestTimeToBuySellStock2
    {
        public int MaxProfit(int[] prices)
        {
            var profit = 0;

            var prevPrice = prices[0];
            int? buyPrice = null;

            for (var i = 1; i < prices.Length; i++)
            {
                var currPrice = prices[i];

                if (buyPrice != null && prevPrice >= currPrice)
                {
                    profit += prevPrice - buyPrice.Value;
                    buyPrice = null;
                }
                else if (buyPrice == null && prevPrice < currPrice)
                {
                    buyPrice = prevPrice;
                }

                prevPrice = currPrice;
            }

            if (buyPrice != null) profit += prices[^1] - buyPrice.Value;

            return profit;
        }
    }
}