using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FintechExam
{
    // paper with n numbers
    // can repeat next action k times:
    // replace a digit in any number
    // find maximum difference original numbers sum and after k actions
    public class FirstTask
    {
        public static void Run()
        {
            var nk = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();
            var n = nk[0];
            var k = nk[1];
            var numbers = Console.ReadLine()
                .Split(" ")
                .ToList();

            Console.WriteLine(Run(n, k, numbers));
        }

        public static long Run(int n, int k, List<string> numbers)
        {
            var myStructs = numbers
                .Select(s => new MyStruct
                {
                    Digits = s.Select(c => (byte) char.GetNumericValue(c)).ToArray()
                })
                .ToArray();
            var queue = new PriorityQueue<MyStruct>(myStructs.Length, Comparer<MyStruct>.Default);
            foreach (var myStruct in myStructs)
            {
                queue.Push(myStruct);
            }

            long result = 0;
            for (int i = 0; i < k; i++)
            {
                if (queue.Count == 0 || queue.Top.MaxGain == 0)
                    break;

                var curr = queue.Top;
                result += curr.MaxGain;
                queue.Pop();
                curr.Update();
                queue.Push(curr);
            }
            return result;
        }
    }

    public class PriorityQueue<T>
    {
        #region constructors
 
        internal PriorityQueue(int capacity, IComparer<T> comparer)
        {
            _heap = new T[capacity > 0 ? capacity : DefaultCapacity];
            _count = 0;
            _comparer = comparer;
        }
 
        #endregion
 
        #region internal members
 
        /// <summary>
        /// Gets the number of items in the priority queue.
        /// </summary>
        internal int Count
        {
            get { return _count; }
        }
 
        /// <summary>
        /// Gets the first or topmost object in the priority queue, which is the
        /// object with the minimum value.
        /// </summary>
        internal T Top
        {
            get
            {
                Debug.Assert(_count > 0);
                if (!_isHeap)
                {
                    Heapify();
                }
 
                return _heap[0];
            }
        }
 
        /// <summary>
        /// Adds an object to the priority queue.
        /// </summary>
        internal void Push(T value)
        {
            // Increase the size of the array if necessary.
            if (_count == _heap.Length)
            {
                Array.Resize<T>(ref _heap, _count * 2);
            }
 
            // A common usage is to Push N items, then Pop them.  Optimize for that
            // case by treating Push as a simple append until the first Top or Pop,
            // which establishes the heap property.  After that, Push needs
            // to maintain the heap property.
            if (_isHeap)
            {
                SiftUp(_count, ref value, 0);
            }
            else
            {
                _heap[_count] = value;
            }
 
            _count++;
        }
 
        /// <summary>
        /// Removes the first node (i.e., the logical root) from the heap.
        /// </summary>
        internal void Pop()
        {
            if (!_isHeap)
            {
                Heapify();
            }
 
            if (_count > 0)
            {
                --_count;
 
                // discarding the root creates a gap at position 0.  We fill the
                // gap with the item x from the last position, after first sifting
                // the gap to a position where inserting x will maintain the
                // heap property.  This is done in two phases - SiftDown and SiftUp.
                //
                // The one-phase method found in many textbooks does 2 comparisons
                // per level, while this method does only 1.  The one-phase method
                // examines fewer levels than the two-phase method, but it does
                // more comparisons unless x ends up in the top 2/3 of the tree.
                // That accounts for only n^(2/3) items, and x is even more likely
                // to end up near the bottom since it came from the bottom in the
                // first place.  Overall, the two-phase method is noticeably better.
 
                T x = _heap[_count];        // lift item x out from the last position
                int index = SiftDown(0);    // sift the gap at the root down to the bottom
                SiftUp(index, ref x, 0);    // sift the gap up, and insert x in its rightful position
                _heap[_count] = default(T); // don't leak x
            }
        }
 
        #endregion
 
        #region private members
 
        // sift a gap at the given index down to the bottom of the heap,
        // return the resulting index
        private int SiftDown(int index)
        {
            // Loop invariants:
            //
            //  1.  parent is the index of a gap in the logical tree
            //  2.  leftChild is
            //      (a) the index of parent's left child if it has one, or
            //      (b) a value >= _count if parent is a leaf node
            //
            int parent = index;
            int leftChild = HeapLeftChild(parent);
 
            while (leftChild < _count)
            {
                int rightChild = HeapRightFromLeft(leftChild);
                int bestChild =
                    (rightChild < _count && _comparer.Compare(_heap[rightChild], _heap[leftChild]) < 0) ?
                    rightChild : leftChild;
 
                // Promote bestChild to fill the gap left by parent.
                _heap[parent] = _heap[bestChild];
 
                // Restore invariants, i.e., let parent point to the gap.
                parent = bestChild;
                leftChild = HeapLeftChild(parent);
            }
 
            return parent;
        }
 
        // sift a gap at index up until it reaches the correct position for x,
        // or reaches the given boundary.  Place x in the resulting position.
        private void SiftUp(int index, ref T x, int boundary)
        {
            while (index > boundary)
            {
                int parent = HeapParent(index);
                if (_comparer.Compare(_heap[parent], x) > 0)
                {
                    _heap[index] = _heap[parent];
                    index = parent;
                }
                else
                {
                    break;
                }
            }
            _heap[index] = x;
        }
 
        // Establish the heap property:  _heap[k] >= _heap[HeapParent(k)], for 0<k<_count
        // Do this "bottom up", by iterating backwards.  At each iteration, the
        // property inductively holds for k >= HeapLeftChild(i)+2;  the body of
        // the loop extends the property to the children of position i (namely
        // k=HLC(i) and k=HLC(i)+1) by lifting item x out from position i, sifting
        // the resulting gap down to the bottom, then sifting it back up (within
        // the subtree under i) until finding x's rightful position.
        //
        // Iteration i does work proportional to the height (distance to leaf)
        // of the node at position i.  Half the nodes are leaves with height 0;
        // there's nothing to do for these nodes, so we skip them by initializing
        // i to the last non-leaf position.  A quarter of the nodes have height 1,
        // an eigth have height 2, etc. so the total work is ~ 1*n/4 + 2*n/8 +
        // 3*n/16 + ... = O(n).  This is much cheaper than maintaining the
        // heap incrementally during the "Push" phase, which would cost O(n*log n).
        private void Heapify()
        {
            if (!_isHeap)
            {
                for (int i = _count/2 - 1; i >= 0; --i)
                {
                    // we use a two-phase method for the same reason Pop does
                    T x = _heap[i];
                    int index = SiftDown(i);
                    SiftUp(index, ref x, i);
                }
                _isHeap = true;
            }
        }
 
        /// <summary>
        /// Calculate the parent node index given a child node's index, taking advantage
        /// of the "shape" property.
        /// </summary>
        private static int HeapParent(int i)
        {
            return (i - 1) / 2;
        }
 
        /// <summary>
        /// Calculate the left child's index given the parent's index, taking advantage of
        /// the "shape" property. If there is no left child, the return value is >= _count.
        /// </summary>
        private static int HeapLeftChild(int i)
        {
            return (i * 2) + 1;
        }
 
        /// <summary>
        /// Calculate the right child's index from the left child's index, taking advantage
        /// of the "shape" property (i.e., sibling nodes are always adjacent). If there is
        /// no right child, the return value >= _count.
        /// </summary>
        private static int HeapRightFromLeft(int i)
        {
            return i + 1;
        }
 
        private T[] _heap;
        private int _count;
        private IComparer<T> _comparer;
        private bool _isHeap;
        private const int DefaultCapacity = 6;
 
        #endregion
    }

    public struct MyStruct : IComparable<MyStruct>
    {
        private byte[] _digits;
        public int NonNineIndex { get; private set; }
        public int MaxGain { get; private set; }

        public byte[] Digits
        {
            get => _digits;
            set
            {
                _digits = value;
                NonNineIndex = -1;
                MaxGain = CalculateMaxGain();
            }
        }

        public void Update()
        {
            if (NonNineIndex >= 0 && NonNineIndex < _digits.Length)
            {
                _digits[NonNineIndex] = 9;
                MaxGain = CalculateMaxGain();
            }
        }

        private int CalculateMaxGain()
        {
            for (int i = NonNineIndex + 1; i < _digits.Length; i++)
            {
                if (_digits[i] != 9)
                {
                    NonNineIndex = i;
                    return (9 - _digits[i]) * IntPow(10, (uint) (_digits.Length - 1 - NonNineIndex));
                }
            }

            return 0;
        }

        public int CompareTo(MyStruct other)
        {
            return other.MaxGain.CompareTo(MaxGain);
        }

        public override string ToString()
        {
            return $"Gain: {MaxGain}, nums: {string.Join("", Digits)}";
        }
        
        int IntPow(int x, uint pow)
        {
            int ret = 1;
            while ( pow != 0 )
            {
                if ( (pow & 1) == 1 )
                    ret *= x;
                x *= x;
                pow >>= 1;
            }
            return ret;
        }
    }
}