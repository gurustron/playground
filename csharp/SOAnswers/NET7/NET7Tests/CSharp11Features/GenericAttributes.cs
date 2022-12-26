using System.Reflection;

namespace NET7Tests.CSharp11Features;

public class GenericAttributes
{
    [Test]
    public void TestGenericAttributesReflection()
    {
        var testedType = typeof(MyClass);
        
        // Test AllowMultiple=true
        // open generic will throw for multiple closed generic attributes defined 
        Assert.Throws<AmbiguousMatchException>(() => testedType.GetCustomAttribute(typeof(GenericMultipleAttribute<>)));
        
        // can get single closed 
        var customMultipleAttribute1 = testedType.GetCustomAttribute(typeof(GenericMultipleAttribute<MyClass>));
        Assert.IsNotNull(customMultipleAttribute1);
        var customMultipleAttribute2 = testedType.GetCustomAttribute(typeof(GenericMultipleAttribute<int>));
        Assert.IsNotNull(customMultipleAttribute2);

        // gets all open generic type attributes with GetCustomAttributes
        var customMultipleAttributes = testedType.GetCustomAttributes(typeof(GenericMultipleAttribute<>));
        Assert.That(customMultipleAttributes.Count(), Is.EqualTo(2));

        // Test AllowMultiple=false (not that relevant)
        // gets single by open generic type
        var customSingleAttribute = testedType.GetCustomAttribute(typeof(GenericSingleAttribute<>));
        Assert.IsNotNull(customSingleAttribute);
        // gets single by correctly closed type
        var customSingleAttribute1 = testedType.GetCustomAttribute(typeof(GenericSingleAttribute<MyClass>));
        Assert.IsNotNull(customSingleAttribute1);
        // gets none for "wrong" closed type
        var customSingleAttribute2 = testedType.GetCustomAttribute(typeof(GenericSingleAttribute<int>));
        Assert.IsNull(customSingleAttribute2);
    }

    [Test]
    public void TestGenericAttributesReflectionWithInheritance()
    {
        var testedType = typeof(MyClass2);
        
        Assert.IsNotNull(testedType.GetCustomAttribute<ChildClosedGenericSingleAttribute>());
        Assert.IsNotNull(testedType.GetCustomAttribute<ChildOpenGenericSingleAttribute<MyClass2>>());
        Assert.IsNotNull(testedType.GetCustomAttribute(typeof(ChildOpenGenericSingleAttribute<>)));

        // Maybe surprising but as with non-generic ones - inherited attributes are returned when searched for base one
        var customAttributesInherited = testedType.GetCustomAttributes(typeof(GenericSingleAttribute<>), inherit: true);
        var customAttributes = testedType.GetCustomAttributes(typeof(GenericSingleAttribute<>), inherit: false);
        
        Assert.That(customAttributesInherited.Length, Is.EqualTo(2));
        Assert.That(customAttributes.Length, Is.EqualTo(2));
        var childClosedAttribute = testedType.GetCustomAttribute(typeof(GenericSingleAttribute<int>));
        Assert.IsNotNull(childClosedAttribute);
        Assert.That(childClosedAttribute.GetType(), Is.EqualTo(typeof(ChildClosedGenericSingleAttribute)));
        var childOpenAttribute = testedType.GetCustomAttribute(typeof(GenericSingleAttribute<MyClass2>));
        Assert.IsNotNull(childOpenAttribute);
        Assert.That(childOpenAttribute.GetType(), Is.EqualTo(typeof(ChildOpenGenericSingleAttribute<MyClass2>)));
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

[ChildClosedGenericSingle]
[ChildOpenGenericSingle<MyClass2>]
file class MyClass2 {}

file class ChildClosedGenericSingleAttribute : GenericSingleAttribute<int>
{
}

file class ChildOpenGenericSingleAttribute<T> : GenericSingleAttribute<T> 
{
}


// var types = testedType.Assembly
//     .GetTypes()
//     .Where(t => t.Name.Contains("MyClass"))
//     .ToList();