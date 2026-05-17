using System.Globalization;
using WhatsNewNET10.Features;

// NumericOrdering.Do();


string[] things = ["paul", "bob", "lauren", "007", "90"];

StringComparer numericStringComparer = 
    StringComparer.Create(CultureInfo.CurrentCulture, CompareOptions.NumericOrdering);

foreach (var item in things.Order(numericStringComparer))
{
    Console.WriteLine(item);
}