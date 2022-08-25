using System.Text;

namespace LeetCode
{
    public class AddTwoLinked
    {
        public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            var result = new ListNode();
            var curr = result;
            ListNode prev;
            do
            {
                var currSum = (l1?.val ?? 0) + (l2?.val ?? 0) + curr.val;
                curr.val = currSum % 10;
                curr.next = new ListNode(currSum / 10);
                l1 = l1?.next;
                l2 = l2?.next;
                prev = curr;
                curr = curr.next;
            } while (l1 != null || l2 != null);

            if (curr.val == 0) prev.next = null;

            return result;
        }

        public class ListNode
        {
            public ListNode next;
            public int val;

            public ListNode(int val = 0, ListNode next = null)
            {
                this.val = val;
                this.next = next;
            }

            public override string ToString()
            {
                var sb = new StringBuilder();
                var curr = this;
                while (curr is not null)
                {
                    sb.Append(curr.val);
                    sb.Append(" ");
                    curr = curr.next;
                }

                return sb.ToString();
            }
        }
    }
}