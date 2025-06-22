using System.Collections;
using System.Text.Json;
using System.Transactions;
using Xunit.Sdk;

namespace XunitTests;

public class TestDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new List<object[]>
    {
        new object[] {5, 1, 3, 9},
        new object[] {7, 1, 5, 3}
    };

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class ParameterizedTests
{
    public bool IsOddNumber(int number)
    {
        return number % 2 != 0;
    }

    [Theory]
    [ClassData(typeof(TestDataGenerator))]
    public void AllNumbers_AreOdd_WithClassData(int a, int b, int c, int d)
    {
        Assert.True(IsOddNumber(a));
        Assert.True(IsOddNumber(b));
        Assert.True(IsOddNumber(c));
        Assert.True(IsOddNumber(d));
    }
}

public class TestDataGeneratorUsingTheoryData : TheoryData<int, int, int, int>
{
    public TestDataGeneratorUsingTheoryData()
    {
        Add(5, 1, 3, 9);
        Add(7, 1, 5, 3);
    }
}

public class ParameterizedTestsWithTheoryData
{
    public static TheoryData<int, int, IDictionary<string, string>> TestData => new()
    {

    };

    public bool IsOddNumber(int number)
    {
        return number % 2 != 0;
    }

    [Theory]
    [ClassData(typeof(TestDataGeneratorUsingTheoryData))]
    public void AllNumbers_AreOdd_WithTheoryData(int a, int b, int c, int d)
    {
        Assert.True(IsOddNumber(a));
        Assert.True(IsOddNumber(b));
        Assert.True(IsOddNumber(c));
        Assert.True(IsOddNumber(d));
    }
}

public class TestClass
{
    public interface IMyDict<TKey, TValue> : IDictionary<TKey, TValue>, IXunitSerializable;

    class MyDict<TKey, TValue> : Dictionary<TKey, TValue>,  IMyDict<TKey, TValue> 
    {
        public void Deserialize(IXunitSerializationInfo info)
        {
            var keys = JsonSerializer.Deserialize<TKey[]>(info.GetValue<string>("_DictKeys"));
            foreach (var key in keys)
            {
                Add(key, info.GetValue<TValue>(JsonSerializer.Serialize(key)));
            }
        }

        public void Serialize(IXunitSerializationInfo info)
        {
            info.AddValue("_DictKeys", JsonSerializer.Serialize(Keys));
            foreach (var key in Keys)
            {
                info.AddValue(JsonSerializer.Serialize(key), this[key]);
            }
        }
    }
    public static TheoryData<int, int, IMyDict<string, string>> TestData =>
    [
        (1, 1, new MyDict<string, string> { { "a", "a" } })
    ];
    
    [Theory]
    [MemberData(nameof(TestData))]
    public void TestMethod(int i, int j, IDictionary<string, string> test)
    {
        int ii = i;
        var x = test;
    }
}