using System.Diagnostics.CodeAnalysis;

namespace NET7Tests.CSharp11Features;

public class RequiredMembersTests
{
    [Test]
    public void TestNotInit()
    {
        var myClass = new MyClass("");
        
        Assert.IsNull(myClass.Name);
    }
}

file class MyClass
{
    public required string? Name { get; set; }

    public MyClass()
    {
    }

    // C# 11 - completely disables all required checks
    [SetsRequiredMembers]
    public MyClass(string test)
    {
        
    }
}