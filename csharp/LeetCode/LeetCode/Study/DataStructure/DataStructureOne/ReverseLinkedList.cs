namespace LeetCode.Study.DataStructure.DataStructureOne;

public class ReverseLinkedList
{
    public ListNode ReverseList(ListNode head) 
    {
        if (head is null)
        {
            return head;
        }

        ListNode result = null;
        ListNode ReverseListIteration(ListNode curr)
        {
            if (curr.next is not null)
            {
                var parent = ReverseListIteration(curr.next);
                parent.next = curr;
            }
            else
            {
                result = curr;
            }
            return curr;
        }

        ReverseListIteration(head);
        head.next = null;
        return result;
    }
    
    public class ListNode
    {
        public int val;
        public ListNode next;

        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
    }
}