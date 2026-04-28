using System.Globalization;

namespace WhatsNewNET10.Features;

public class NumericOrdering
{
    public static void Do()
    {
        StringComparer numericStringComparer = 
            StringComparer.Create(CultureInfo.CurrentCulture, CompareOptions.NumericOrdering);

        Console.WriteLine(numericStringComparer.Equals("02", "2"));
        // Output: True

        foreach (string os in new[] { "Windows 8", "Windows 10", "Windows 11" }.Order(numericStringComparer))
        {
            Console.WriteLine(os);
        }

        // Output:
        // Windows 8
        // Windows 10
        // Windows 11

        HashSet<string> set = new HashSet<string>(numericStringComparer) { "007" };
        Console.WriteLine(set.Contains("7"));
        // Output: True
    }
}