// See https://aka.ms/new-console-template for more information

using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using NET7Bench.Benchs;

Console.WriteLine("Hello, World!");

var summary = BenchmarkRunner.Run<EnBench>();


public class EnBench
{
    [Benchmark]
    public void EnumerateSample()
    {

        var items = new int[1000000];
        var enumerator = new Enumerator<int>(items);

        // MoveNext and .Current will not inline, bad 
        // Address jumps on every single item, incredible bad
        while (enumerator.MoveNext())
        {
            ref var i = ref enumerator.Current;
            i++;
        }
    }
    [Benchmark]
    public void FillArray()
    {
        var items = new int[1000000];
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = 1;
        }
    }
    
    /// <summary>
    ///     The <see cref="Enumerator{T}"/> struct
    ///     represents an enumerator with which one can iterate over all items of an array or span.
    /// </summary>
    /// <typeparam name="T">The generic type.</typeparam>
    public ref struct Enumerator<T>
    {
        private readonly Span<T> _span;

        private int _index;
        private readonly int _size;
        

        /// <summary>
        ///     Initializes a new instance of the <see cref="Enumerator{T}"/> struct.
        /// </summary>
        /// <param name="span">The <see cref="Span{T}"/> with items to iterate over.</param>
        public Enumerator(Span<T> span)
        {
            _span = span;
            _index = -1;
            _size = span.Length;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Enumerator{T}"/> struct.
        /// </summary>
        /// <param name="span">The <see cref="Span{T}"/> with items to iterate over.</param>
        /// <param name="length">Its length or size.</param>
        public Enumerator(Span<T> span, int length)
        {
            _span = span;
            _index = -1;
            _size = length;
        }

        /// <summary>
        ///     Moves to the next item.
        /// </summary>
        /// <returns>True if there still items, otherwhise false.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext()
        {
            return unchecked(++_index) < _size;
        }

        /// <summary>
        ///     Resets this instance.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
        {
            _index = -1;
        }

        /// <summary>
        ///     Returns a reference to the current item.
        /// </summary>
        public readonly ref T Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ref _span[_index];
        }
    }


}