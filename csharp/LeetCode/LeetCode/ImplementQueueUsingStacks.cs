using System.Collections.Generic;
using System.Linq;

namespace LeetCode;

/// <summary>
/// https://leetcode.com/problems/implement-queue-using-stacks
/// </summary>
public class ImplementQueueUsingStacks
{
    public class MyQueue
    {
        private Stack<int> First = new();
        private Stack<int> Second = new();

        public void Push(int x) => First.Push(x);

        public int Pop()
        {
            Move();
            return Second.Pop();
        }

        public int Peek()
        {
            Move();
            return Second.Peek();
        }

        public bool Empty() => First.Count == 0 && Second.Count == 0;

        private void Move()
        {
            if (!Second.Any())
            {
                while (First.TryPop(out var i))
                {
                    Second.Push(i);
                }
            }
        }
    }
}