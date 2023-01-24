using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace LeetCode.Study.DataStructure.DataStructureTwo;
#nullable enable
public class MyHashMap
{
    private List<(int, int)>?[] storage = new List<(int, int)>[11_777];     

    public MyHashMap() 
    {
        
    }
    
    public void Put(int key, int value)
    {
        var index = CalcIndex(key);
        storage[index] ??= new();

        var bucket = storage[index];
        for (int i = 0; i < bucket!.Count; i++)
        {
            var (iKey, _) = bucket[i];
            if (iKey == key)
            {
                bucket[i] = (key, value);
                return;
            }
        }
        
        bucket.Add((key, value));
    }
    
    public int Get(int key) 
    {
        var index = CalcIndex(key);
        var bucket = storage[index];
        if (bucket is null) return -1;
        
        for (int i = 0; i < bucket!.Count; i++)
        {
            var (iKey, value) = bucket[i];
            if (iKey == key)
            {
                return value;
            }
        }

        return -1;
    }
    
    public void Remove(int key) 
    {
        var index = CalcIndex(key);
        var bucket = storage[index];
        if (bucket is null) return;
        
        for (int i = 0; i < bucket!.Count; i++)
        {
            var (iKey, value) = bucket[i];
            if (iKey == key)
            {
                bucket.RemoveAt(i);
                break;
            }
        }
    }

    private int CalcIndex(int key) => key.GetHashCode() % storage.Length;
}
