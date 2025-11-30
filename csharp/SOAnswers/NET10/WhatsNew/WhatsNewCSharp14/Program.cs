using System.Runtime.CompilerServices;
using WhatsNewCSharp14.Features;
using WhatsNewCSharp14.Tests;

// int doSomething = ExtensionMembers.DoSomething();
// FieldKeyword.Do();
// FirstClassSpans.Do();
// UserDefinedCompoundAssignment.Do();

// Unbound generic types allowed in the nameof
Console.WriteLine(nameof(List<>)); // Prints "List"

// The null-conditional member access operators, ?. and ?[],
// can now be used on the left hand side of an assignment or compound assignment.
List<int>? list = null;
list?.Capacity = 10;
list?.Capacity += 10;
list?[0] = 1;

list = new();
list?.Capacity = 10;
list?.Capacity += 10;
list?.Add(7);
list?[0] = 42;
Console.WriteLine(list.Capacity);
Console.WriteLine(list[0]);