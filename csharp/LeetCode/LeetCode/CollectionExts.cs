using System.Collections.Generic;

namespace LeetCode
{
    public static class CollectionExts
    {
        public static string ToPrintVersion<T>(this IEnumerable<T> col)
        {
            return string.Join(", ", col);
        }
    }
}