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
        1L.BasicLog1();
        
        var basicLog = Helpers.BasicLog((short)1);
        Assert.Pass();
    }
}
