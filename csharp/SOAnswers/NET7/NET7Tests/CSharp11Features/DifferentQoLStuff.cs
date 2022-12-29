using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace NET7Tests.CSharp11Features;

public class DifferentQoLStuff
{
    [Test]
    public void TestNameOf()
    {
        var s = "";
        var length = TestMethod(s).Length; // no warning, TestMethod(null).Length will have one

        [return: NotNullIfNotNull(nameof(name))]
        string? TestMethod(string? name) => name;
    }
    
    [Test]
    public void TestStringLiterals()
    {
        Assert.That("Hello World!"u8.Length, Is.EqualTo(12));
    }
}