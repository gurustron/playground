namespace NET7Tests.CSharp11Features;

public class StructTests
{
    [Test]
    public void TestAssignementAndDefaultCtor()
    {
        var m1 = new MeasurementNoDefCtor(5);
        var m1Def = new MeasurementDefCtor(5);
        Assert.That(m1.Value, Is.EqualTo(5));  // output: 5 (Ordinary measurement)
        Assert.That(m1Def.Value, Is.EqualTo(5));  // output: 5 (Ordinary measurement)
        Assert.That(m1.Description, Is.EqualTo("Ordinary measurement"));  
        Assert.That(m1Def.Description, Is.EqualTo("Ordinary measurement"));  

        // https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-10.0/parameterless-struct-constructors
        var m2 = new MeasurementNoDefCtor();
        var m2Def = new MeasurementDefCtor();
        Assert.That(m2.Value, Is.EqualTo(0));
        Assert.That(m2Def.Value, Is.EqualTo(0));
        Assert.IsNull(m2.Description);  
        Assert.That(m2Def.Description, Is.EqualTo("Ordinary measurement")); 

        var m3 = default(MeasurementNoDefCtor);
        var m3Arr = (new MeasurementNoDefCtor[1])[0];
        var m3Def = default(MeasurementDefCtor);
        var m3DefArr = (new MeasurementDefCtor[1])[0];
        Assert.That(m3.Value, Is.EqualTo(0));
        Assert.That(m3Arr.Value, Is.EqualTo(0));
        Assert.That(m3Def.Value, Is.EqualTo(0));
        Assert.That(m3DefArr.Value, Is.EqualTo(0));
        Assert.IsNull(m3.Description);  
        Assert.IsNull(m3Arr.Description);  
        Assert.IsNull(m3Def.Description);  
        Assert.IsNull(m3DefArr.Description);  
    }
    
    public readonly struct MeasurementNoDefCtor
    {
        public MeasurementNoDefCtor(double value)
        {
            Value = value;
        }

        public MeasurementNoDefCtor(double value, string description)
        {
            Value = value;
            Description = description;
        }

        public MeasurementNoDefCtor(string description)
        {
            Description = description;
        }

        public double Value { get; init; }
        public string Description { get; init; } = "Ordinary measurement";

        public override string ToString() => $"{Value} ({Description})";
    }
    
    public readonly struct MeasurementDefCtor
    {
        public MeasurementDefCtor()
        {
        }

        public MeasurementDefCtor(double value)
        {
            Value = value;
        }

        public MeasurementDefCtor(double value, string description)
        {
            Value = value;
            Description = description;
        }

        public MeasurementDefCtor(string description)
        {
            Description = description;
        }

        public double Value { get; init; }
        public string Description { get; init; } = "Ordinary measurement";

        public override string ToString() => $"{Value} ({Description})";
    }

}