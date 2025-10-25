using Moq;
using Range = System.Range;

namespace NET9Console.Tests;
class MyTests
{
    [Test]
    public void MyTestClassTest()
    {
        var myMock = new Mock<IMyInterface>(MockBehavior.Strict);
        var sequence = new MockSequence();
        myMock
            .InSequence(sequence)
            .Setup(s => s.Method("value1", "value2", null))
            .Verifiable(Times.Once);
        myMock
            .InSequence(sequence)
            .Setup(s => s.Method("value2", "value3", null))
            .Verifiable(Times.Once);

        var myTestClass = new MyTestClass(myMock.Object);
        myTestClass.MyMethod();

        var conditionalSetups = myMock.Setups
            .Where(setup => setup is { IsConditional: true, IsOverridden: false });
        foreach (var conditionalSetup in conditionalSetups)
        {
            conditionalSetup.Verify();
        }
        // myMock.Verify();
    }
}
public interface IMyInterface
{
    void Method(string a, string b, CancellationToken? cancellationToken = null);
}

public class MyTestClass
{
    private readonly IMyInterface _myInterface;
    public MyTestClass(IMyInterface myInterface) { _myInterface = myInterface; }

    public void MyMethod()
    {
        _myInterface.Method("value1", "value2");
        _myInterface.Method("value2", "value3");
    }
}

