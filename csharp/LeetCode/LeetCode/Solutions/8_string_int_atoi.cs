using System;
using System.Numerics;

namespace EightStringIntAtoi;

public class Solution 
{
    public int MyAtoi(string s) 
    {
        var span = s.AsSpan();
        int position = 0;
        int start;
        int count = 0;
        bool digitEncountered = false;
        while(position < span.Length && span[position] == ' ') {
            position++;
        }

        if(position >= span.Length) return 0;

        start = position;
        
        if(span[position] is '+' or '-')
        {
            start = position;
            position++;
            count++;
        }

        while(position < span.Length && char.IsDigit(span[position]))
        {
            digitEncountered = true;
            position++;
            count++;
        }

        if(!digitEncountered) return 0;

        return int.CreateSaturating(BigInteger.Parse(span.Slice(start, count)));
    }
}