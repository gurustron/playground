namespace WhatsNewCSharp14.Features;

public static class UserDefinedCompoundAssignment
{
    public static void Do()
    {
        CompoundAssignment left = new();
        CompoundAssignment right = new();
        var bytes = GC.GetTotalAllocatedBytes(true);
        for (int i = 0; i < 10_000; i++)
        {
            left += right;
        }

        Console.WriteLine(GC.GetTotalAllocatedBytes(true) - bytes); // Prints "0"
        
        NoCompoundAssignment leftN = new();
        NoCompoundAssignment rightN = new();
        bytes = GC.GetTotalAllocatedBytes(true);
        for (int i = 0; i < 10_000; i++)
        {
            leftN += rightN;
        }

        Console.WriteLine(GC.GetTotalAllocatedBytes(true) - bytes); // Prints "240000"
        
        // Used in System.Numerics.Tensors
    }
}

file class CompoundAssignment
{
    public int Value { get; set; }
    
    public void operator+=(CompoundAssignment right) => Value += right.Value;
}

file class NoCompoundAssignment
{
    public int Value { get; init; }
    
    public static NoCompoundAssignment operator+(NoCompoundAssignment left, NoCompoundAssignment right) 
        => new() { Value = left.Value + right.Value};
}