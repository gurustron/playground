using System.Collections.ObjectModel;
using BenchmarkDotNet.Attributes;

namespace NET10.Benchs.Devirtualization;

[HideColumns("Job", "Error", "StdDev", "Median", "RatioSD")]
public class ReadOnlyCollectionEnumeratorDevirtualizationBench
{
    private readonly ReadOnlyCollection<int> _listViaArray
        = new(Enumerable.Range(1, 1000).ToArray());
    private readonly ReadOnlyCollection<int> _listViaList 
        = new(Enumerable.Range(1, 1000).ToList());

    [Benchmark]
    public int SumEnumerableViaArray()
    {
        int sum = 0;
        foreach (var item in _listViaArray)
        {
            sum += item;
        }

        return sum;
    }

    [Benchmark]
    public int SumForLoopViaArray()
    {
        ReadOnlyCollection<int> list = _listViaArray;
        int sum = 0;
        int count = list.Count;
        for (int i = 0; i < count; i++)
        {
            sum += _listViaArray[i];
        }

        return sum;
    }    
    
    [Benchmark]
    public int SumEnumerableViaList()
    {
        int sum = 0;
        foreach (var item in _listViaList)
        {
            sum += item;
        }

        return sum;
    }

    [Benchmark]
    public int SumForLoopViaList()
    {
        ReadOnlyCollection<int> list = _listViaList;
        int sum = 0;
        int count = list.Count;
        for (int i = 0; i < count; i++)
        {
            sum += _listViaList[i];
        }

        return sum;
    }
}