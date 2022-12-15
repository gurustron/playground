using System.Linq;

namespace LeetCode.Study.DataStructure.DataStructureOne;

/// <summary>
/// https://leetcode.com/problems/ransom-note/description/
/// </summary>
public class ValidAnagram
{
    public bool IsAnagram(string s, string t)
    {
        if (s.Length != t.Length)
        {
            return false;
        }
        
        var countsDict = t
            .GroupBy(c => c)
            .ToDictionary(g => g.Key, g => g.Count());

        foreach (var c in s)
        {
            if (!countsDict.TryGetValue(c, out var count) || count <= 0)
            {
                return false;
            }

            countsDict[c] = count - 1;
        }

        return true;
    }
}