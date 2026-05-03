using System.Linq;

namespace SixtyOneRotateList;


public class Solution
{
    public ListNode RotateRight(ListNode head, int k)
    {
        if(head is null || head.next is null || k == 0 )
        {
            return head;
        }

        var last = head;
        var length = 1;
        while(last.next is not null)
        {
            length++;
            last = last.next;
        }

        var toSkip = length - (k % length);
        
        if(toSkip == 0)
        {
            return head;
        }

        var newLast = head;
        for(int i = 1; i < toSkip; i++)
        {
            newLast = newLast.next;
        }

        var newStart = newLast.next;
        newLast.next = null;
        last.next = head;

        return newStart;
    }

    // Definition for singly-linked list.
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

    public (ListNode start, int numOfRotations) CreateTask(int numOfNodes, int numOfRotations)
    {
        var start = Enumerable.Range(0, numOfNodes - 1)
            .Reverse()
            .Aggregate(new ListNode(numOfNodes - 1), (agg, i) => new ListNode(i, agg));

        return (start, numOfRotations);
    }
}