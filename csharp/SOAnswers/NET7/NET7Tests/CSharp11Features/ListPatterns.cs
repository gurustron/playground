namespace NET7Tests.CSharp11Features;

public class ListPatterns
{
    [Test]
    public void TestListPatterns()
    {
        var list = new List<int>();
        Assert.IsTrue(list is [], "list is Empty");
        IList<int> enumerable = Enumerable.Range(0, 10).ToList();
        Assert.IsTrue(enumerable is [0, ..], "enumerable has at least 1 item which is 0");
        Assert.IsTrue(enumerable is [.., 9], "enumerable has at least 1 item and ends with 9");
        // Slice patterns may not be used for a value of type 'System.Collections.Generic.List<int>'. No suitable range indexer or 'Slice' method was found:
        // enumerable is [0, 1, ..{Count: > 5}, 9]
        Assert.IsTrue(enumerable.ToArray() is [0, 1, .. {Length: > 5}, 9], "enumerable starts with 0, 1, ends with 9 and has at least 5 items in-between");
    }
}
