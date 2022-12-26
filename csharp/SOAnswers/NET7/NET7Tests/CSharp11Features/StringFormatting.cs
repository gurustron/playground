namespace NET7Tests.CSharp11Features;

public class StringFormatting
{
    [TestCase(1, ExpectedResult = "This is: One")]
    [TestCase(2, ExpectedResult = "This is: Two")]
    [TestCase(3, ExpectedResult = "This is: A lot")]
    public string TestMultilineStringInterpolation(int i) => $"This is: {i switch {
        <= 0 => "LE 0",
        1 => "One",
        2 => "Two",
        _ => "A lot"
    }}";

    [Test]
    public void RawStringLiterals()
    {
        var expected = "This is quote: \"" + Environment.NewLine +
                       "And this is new line";

        var list = new List<string>
        {
            $"""This is quote: "{Environment.NewLine}And this is new line""", // single line 
            """
            This is quote: "
            And this is new line
            """,
            """
This is quote: "
And this is new line
""",
"""
This is quote: "
And this is new line
"""
        };

        foreach (var (s,i) in list.Select((s, i) => (s, i)))
        {
            Assert.That(s, Is.EqualTo(expected), i.ToString());
        }

        var expected2 = "Ends with quote\"";
        var list2 = new List<string>
        {
            $"""Ends with quote"{string.Empty}""",
            """
            Ends with quote"
            """
        };
        
        foreach (var (s,i) in list2.Select((s, i) => (s, i)))
        {
            Assert.That(s, Is.EqualTo(expected2), $"Expected 2: {i}");
        }
    }
}