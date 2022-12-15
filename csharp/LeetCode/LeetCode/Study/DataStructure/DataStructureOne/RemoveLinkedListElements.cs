namespace LeetCode.Study.DataStructure.DataStructureOne;

public class RemoveLinkedListElements
{
    public ListNode RemoveElements(ListNode head, int val)
    {
        var curr = head;
        ListNode result = null;
        ListNode resultCurr = null;

        while (curr is not null)
        {
            if (curr.val != val)
            {
                if (result is null)
                {
                    result = new ListNode(curr.val);
                    resultCurr = result;
                }
                else
                {
                    resultCurr.next = new ListNode(curr.val);
                    resultCurr = resultCurr.next;
                }
            }
            curr = curr.next;
        }
        
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

