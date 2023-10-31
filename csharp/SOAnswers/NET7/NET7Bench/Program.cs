// See https://aka.ms/new-console-template for more information

using System.Numerics;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using NET7Bench.Benchs;

Console.WriteLine("Hello, World!");

// var x = new Data[]
// {
//     new() { b0 = 255 },
//     new() { b1 = 255 },
//     new() { b2 = 255 },
//     new() { b3 = 42 },
//     new() { b4 = 255 },
//     new() { b5 = 255 },
//     new() { b6 = 1 },
//     new() { b7 = 255 },
//     new() { b8 = 255 },
//     new() { b1 = 7, b8 = 255 },
//     new(),
// };
// var y = new Data();
// Console.WriteLine(Unsafe.SizeOf<Data>());
// foreach (var data1 in x)
// {
//     var data = data1;
//     var data2 = data1;
//     Console.WriteLine(UnmanagedExtensions.ViaVector(ref Unsafe.As<Data, byte>(ref data), ref Unsafe.As<Data, byte>(ref data2), (uint)Unsafe.SizeOf<Data>()));
//     Console.WriteLine(UnmanagedExtensions.ViaVector(ref Unsafe.As<Data, byte>(ref data), ref Unsafe.As<Data, byte>(ref y), (uint)Unsafe.SizeOf<Data>()));
// }

var summary = BenchmarkRunner.Run<StructEqualityBench>();

public class CalcBench
{
    const byte hfm = 0b0001_0000;
  
    UInt16 operand1 = 0x2;
    UInt16 operand2 = 0xFFFE;
    private UInt32 result;

    public CalcBench()
    {
        result = (UInt32)(operand1 + operand2);
    }
    
    
    [Benchmark]
    public UInt32 First()
    {
        UInt32 hf = 0;
        for (int i = 0; i < 10_000; i++)
            hf = (((operand1 ^ result ^ operand2) >> 8) & hfm);
        return hf;
    }

    [Benchmark]
    public UInt32 Second()
    {
        UInt32 hf = 0;
        for(int i=0; i < 10_000; i++)
         hf = (result >> 8) & hfm;
        return hf;
    }

    
    // [Benchmark]
    // public UInt32 Second1()
    // {
    //     UInt32 hf = 0;
    //     for(int i=0; i < 10_000; i++)
    //         hf = (result >> 8) & hfm;
    //     return hf;
    // }
    //
    // [Benchmark]
    // public UInt32 First1()
    // {
    //     UInt32 hf = 0;
    //     for (int i = 0; i < 10_000; i++)
    //         hf = (((operand1 ^ result ^ operand2) >> 8) & hfm);
    //     return hf;
    // }
}


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