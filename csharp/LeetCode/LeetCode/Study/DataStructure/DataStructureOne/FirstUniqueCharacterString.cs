using System.Linq;

namespace LeetCode.Study.DataStructure.DataStructureOne;

public class FirstUniqueCharacterString
{
    public int FirstUniqChar(string s)
    {
        var dictionary = s
            .GroupBy(c => c)
            .ToDictionary(grp => grp.Key, grp => grp.Count());
        for (int i = 0; i < s.Length; i++)
        {
            if (dictionary[s[i]] == 1)
            {
                return i;
            }
        }

        return -1;
    }
}