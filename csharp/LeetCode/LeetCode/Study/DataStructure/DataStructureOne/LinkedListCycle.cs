using System.Collections.Generic;

namespace LeetCode.Study.DataStructure.DataStructureOne;

/// <summary>
/// https://leetcode.com/problems/linked-list-cycle/
/// </summary>
public class LinkedListCycle
{
    public bool HasCycle(ListNode head)
    {
        var visited = new HashSet<ListNode>();

        while (head is not null)
        {
            if (visited.Contains(head))
            {
                return true;
            }

            visited.Add(head);
            head = head.next;
        }

        return false;
    }

    public bool HasCycle1(ListNode head)
    {
        if (head == null) return false;
        ListNode fast = head.next;
        ListNode slow = head;
        while (fast != slow)
        {
            if (fast == null || fast.next == null)
            {
                return false;
            }

            fast = fast.next.next;
            slow = slow.next;
        }

        return true;
    }

    public class ListNode
    {
        public ListNode next;
        public int val;

        public ListNode(int x)
        {
            val = x;
            next = null;
        }
    }
}