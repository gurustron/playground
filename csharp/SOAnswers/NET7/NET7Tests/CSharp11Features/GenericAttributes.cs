using System.Reflection;

namespace NET7Tests.CSharp11Features;

public class GenericAttributes
{
    [Test]
    public void TestGenericAttributesReflection()
    {
        var testedType = typeof(MyClass);
        // var types = testedType.Assembly
        //     .GetTypes()
        //     .Where(t => t.Name.Contains("MyClass"))
        //     .ToList();

        Assert.Throws<AmbiguousMatchException>(() => testedType.GetCustomAttribute(typeof(GenericMultipleAttribute<>)));
        var customMultipleAttribute1 = testedType.GetCustomAttribute(typeof(GenericMultipleAttribute<MyClass>));
        Assert.IsNotNull(customMultipleAttribute1);
        var customMultipleAttribute2 = testedType.GetCustomAttribute(typeof(GenericMultipleAttribute<int>));
        Assert.IsNotNull(customMultipleAttribute2);

        var customMultipleAttributes = testedType.GetCustomAttributes(typeof(GenericMultipleAttribute<>));
        Assert.That(customMultipleAttributes.Count(), Is.EqualTo(2));

        var customSingleAttribute = testedType.GetCustomAttribute(typeof(GenericSingleAttribute<>));
        Assert.IsNotNull(customSingleAttribute);
        var customSingleAttribute1 = testedType.GetCustomAttribute(typeof(GenericSingleAttribute<MyClass>));
        Assert.IsNotNull(customSingleAttribute1);
        var customSingleAttribute2 = testedType.GetCustomAttribute(typeof(GenericSingleAttribute<int>));
        Assert.IsNull(customSingleAttribute2);
    }
}

[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
file class GenericMultipleAttribute<T> : Attribute { }

[AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
file class GenericSingleAttribute<T> : Attribute { }

[GenericSingle<MyClass>]
[GenericMultiple<MyClass>]
[GenericMultiple<int>]
file class MyClass{}