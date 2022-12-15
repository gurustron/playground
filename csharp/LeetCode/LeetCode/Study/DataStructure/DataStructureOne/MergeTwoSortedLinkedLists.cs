using System.Diagnostics;

namespace LeetCode.Study.DataStructure.DataStructureOne;

/// <summary>
/// https://leetcode.com/problems/merge-two-sorted-lists/description/
/// </summary>
public class MergeTwoSortedLinkedLists
{
    public ListNode MergeTwoLists(ListNode list1, ListNode list2)
    {
        ListNode head = null;
        if (list1 is null && list2 is null)
        {
            return head;
        }
        
        if (list2 is null || list1?.val < list2.val)
        {
            head = list1;
            list1 = list1.next;
        }
        else 
        {
            head = list2;
            list2 = list2.next;
        }
        
        var curr = head;

        while (list1 is not null || list2 is not null)
        {
            if (list1 is null ||  list2?.val < list1.val)
            {
                curr.next = list2;
                curr = list2;
                list2 = list2.next;
            }
            else
            {
                curr.next = list1;
                curr = list1;
                list1 = list1.next;
            }
        }

        return head;
    }

    [DebuggerDisplay("{ToString()}")]
    public class ListNode
    {
        public int val;
        public ListNode next;

        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }

        public override string ToString()
        {
            return val + " -> " + next?.ToString();
        }
    }
}