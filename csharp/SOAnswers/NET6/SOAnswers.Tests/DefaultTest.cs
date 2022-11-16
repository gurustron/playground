using System.Collections;

namespace SOAnswers.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        string[,] expected = new string[,] { { "value1", "value2" } };
        var actual = expected;
        var throws = new ArrayList(actual);
        CollectionAssert.AreEquivalent(expected, actual);   // throws RankException
        1L.BasicLog1();
        
        var basicLog = Helpers.BasicLog((short)1);
        Assert.Pass();
    }
}
