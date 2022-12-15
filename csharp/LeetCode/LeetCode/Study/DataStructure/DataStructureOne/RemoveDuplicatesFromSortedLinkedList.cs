namespace LeetCode.Study.DataStructure.DataStructureOne;

public class RemoveDuplicatesFromSortedLinkedList
{
    public ListNode DeleteDuplicates(ListNode head)
    {
        if (head is null)
        {
            return head;
        }
        
        ListNode curr = head;
        ListNode next = head.next;
        while (next is not null)
        {
            if (curr.val != next.val)
            {
                curr.next = next;
                curr = next;
            }

            next = next.next;
        }

        curr.next = null;

        return head;
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