using System.Linq;

namespace LeetCode.Study.DataStructure.DataStructureOne;

/// <summary>
/// https://leetcode.com/problems/ransom-note/description/
/// </summary>
public class RansomNote
{
    public bool CanConstruct(string ransomNote, string magazine)
    {
        var countsDict = magazine
            .GroupBy(c => c)
            .ToDictionary(g => g.Key, g => g.Count());

        foreach (var c in ransomNote)
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