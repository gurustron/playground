using System.Collections;

using Moq;

namespace SOAnswers.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task Test1()
    {
        string[,] expected = new string[,] { { "value1", "value2" } };
        var actual = expected;
        var throws = new ArrayList(actual);
        CollectionAssert.AreEquivalent(expected, actual);   // throws RankException
        1L.BasicLog1();
        
        var basicLog = Helpers.BasicLog((short)1);

        var mock = new Mock<IMyClass>();
    mock.Setup(c => c.MeasureAsync(It.IsAny<Func<Task<It.IsAnyType>>>()))
        .Returns(new InvocationFunc(invocation =>
        {
            var arg = (Func<Task>)invocation.Arguments[0];
            return arg.Invoke();
        }));;
    
    
    var measureAsync = mock.Object.MeasureAsync(() => Task.FromResult(42));
    var b = measureAsync is null;
    var result = await measureAsync;
    }

    public interface IMyClass
    {
        Task<T> MeasureAsync<T>(Func<Task<T>> sendFunc);
    }

    public class MyClass : IMyClass
    {
        public async Task<T> MeasureAsync<T>(Func<Task<T>> sendFunc) => await sendFunc();
    }
}
