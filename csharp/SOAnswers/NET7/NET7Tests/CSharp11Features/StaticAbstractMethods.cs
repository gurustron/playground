using System.Reflection;

namespace NET7Tests.CSharp11Features;

public class StaticAbstractMethods
{
    [Test]
    public static void TestReflection()
    {
        Dictionary<string, object> stuff = new()
        {
            { ItemOne.Discriminator, new ItemOne() },
            { ItemTwo.Discriminator, new ItemTwo() },
        };

        foreach ((string key, object value)in stuff)
        {
            var propertyInfo = value.GetType()
                .GetProperty(nameof(IHaveStatic.Discriminator),BindingFlags.Static | BindingFlags.Public);
            Assert.IsNotNull(propertyInfo);
            Assert.That(propertyInfo.GetValue(null), Is.EqualTo(key));
        }
    }

    [Test]
    public void TestCollectionObInterface()
    {
        Dictionary<string, IHaveDisc> stuff = new()
        {
            { ItemOne.Discriminator, new ItemOne() },
            { ItemTwo.Discriminator, new ItemTwo() },
        };
        
        foreach (var (key, value)in stuff)
        {
            Assert.That(value.GetDisc(), Is.EqualTo(key));
        }
    }

    [Test]
    public void TestGeneric()
    {
        Assert.That(GetDisc(new ItemOne()), Is.EqualTo(ItemOne.Discriminator));
        Assert.That(GetDisc(new ItemTwo()), Is.EqualTo(ItemTwo.Discriminator));
        
        static string GetDisc<T>(T item) where T : IHaveStatic => T.Discriminator;   
    }
}

file interface IHaveStatic
{
    public static abstract string Discriminator { get; }
}

file interface IHaveDisc
{
    public string GetDisc();
}

file abstract class ItemBase<T> : IHaveDisc where T : ItemBase<T>, IHaveStatic
{
    public string GetDisc() => T.Discriminator;
}

file class ItemOne : ItemBase<ItemOne>, IHaveStatic
{
    public static string Discriminator => nameof(ItemOne);
}

file class ItemTwo : ItemBase<ItemTwo>, IHaveStatic
{
    public static string Discriminator => nameof(ItemTwo);
}

