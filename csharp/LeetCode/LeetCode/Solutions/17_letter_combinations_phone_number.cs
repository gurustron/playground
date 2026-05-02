using System;
using System.Collections.Generic;
using System.Linq;

namespace SeventeenLetterCombinationsOfPhoneNumber;

public class Solution 
{
    private Dictionary<char, char[]> Map = new()
    {
        {'2', "abc".ToCharArray()},
        {'3', "def".ToCharArray()},
        {'4', "ghi".ToCharArray()},
        {'5', "jkl".ToCharArray()},
        {'6', "mno".ToCharArray()},
        {'7', "pqrs".ToCharArray()},
        {'8', "tuv".ToCharArray()},
        {'9', "wxyz".ToCharArray()}
    };

    public IList<string> LetterCombinations(string digits)
    {
        List<string> agg = new(digits.Select(d => Map[d].Length).Aggregate((acc, next) => acc * next));
        Span<char> currAgg = stackalloc char[digits.Length];
        int v = digits.Length - 1;
        
        void Helper(ref Span<char> prefix, int level)
        {
            if (level == v)
            {
                foreach(var c in Map[digits[v]])
                {
                    prefix[v] = c;
                    agg.Add(new string(prefix));                    
                }
            }
            else
            {
                foreach(var c in Map[digits[level]])
                {
                    prefix[level] = c;
                    Helper(ref prefix, level + 1);
                }
            }
        }

        Helper(ref currAgg, 0);

        return agg;
    }
}