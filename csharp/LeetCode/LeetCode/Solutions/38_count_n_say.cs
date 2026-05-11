namespace ThirtyEightCountAndSay;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

public class Solution
{
    public string CountAndSay(int n)
    {
        Span<char> currChars = stackalloc char[5808];
        Span<char> interimChars = stackalloc char[5808];
        currChars[0] = '1';
        int currLength = 1;
        for (int i = 1; i < n; i++)
        {
            currLength = RLE1(currChars.Slice(0, currLength), interimChars);
            interimChars.Slice(0, currLength).CopyTo(currChars);
        }

        return new string(currChars.Slice(0, currLength));

        int RLE1(ReadOnlySpan<char> source, Span<char> target)
        {
            var prev = source[0];
            var count = 1;
            var written = 0;
            for (int i = 1; i < source.Length; i++)
            {
                var curr = source[i];
                if (curr == prev)
                {
                    count++;
                }
                else
                {
                    Write(count, prev, target);
                    count = 1;
                    prev = curr;
                }
            }


            Write(count, prev, target);

            return written;

            void Write(int count, char c, Span<char> dest)
            {
                // number from .NET source code
                // const int Int32NumberBufferLength = 10 + 1; // 10 for the longest input: 2,147,483,647
                // Span<char> destNum = stackalloc char[Int32NumberBufferLength + 3];

                bool success = count.TryFormat(dest.Slice(written), out var charsWritten);
                Debug.Assert(success);
                written += charsWritten;
                dest[written] = c;
                written++;
            }
        }
    }

    public string CountAndSayOriginal(int n)
    {
        var currStr = "1";

        for (int i = 1; i < n; i++)
        {
            currStr = RLE(currStr);
        }

        return currStr;

        string RLE(string s)
        {
            var agg = new List<char>(s.Length * 2);
            var span = s.AsSpan();
            var prev = span[0];
            var count = 1;
            for (int i = 1; i < span.Length; i++)
            {
                var curr = span[i];
                if (curr == prev)
                {
                    count++;
                }
                else
                {
                    agg.AddRange(count.ToString());
                    agg.Add(prev);
                    count = 1;
                    prev = curr;
                }
            }

            agg.AddRange(count.ToString());
            agg.Add(prev);

            return new string(CollectionsMarshal.AsSpan(agg));
        }
    }
}