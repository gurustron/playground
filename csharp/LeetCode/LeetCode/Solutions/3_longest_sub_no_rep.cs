using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace ThreeLongestSubstringWithoutRepeatingCharacters;

public class Solution 
{
    public int LengthOfLongestSubstring(string s) 
    {
        HashSet<char> chars = new();

        var answer = 0;
        var left = 0;

        for(int right = 0; right < s.Length; right++)
        {
            if (chars.Contains(s[right]))
            {
                while (chars.Contains(s[right]))
                {
                    chars.Remove(s[left]);
                    left++;
                }
            }

            answer = Math.Max(answer, right - left + 1);
            chars.Add(s[right]);
        }

        return answer;
    }

    // public int LengthOfLongestSubstring(string s) 
    // {
    //     var encountered = new HashSet<char>(26);
    //     var answer = 0;

    //     for(int i = 0; i < s.Length; i++)
    //     {
    //         if(s.Length - i < answer) break;
    //         encountered.Clear();
    //         int curr = 0;
    //         for(int j = i; j < s.Length; j++)
    //         {
    //             if(curr + s.Length - j < answer) break;

    //             if (!encountered.Add(s[j])) break;
                
    //             curr++;
    //         }
            
    //         answer = Math.Max(answer, curr);
    //     }

    //     return answer;
    // }
}