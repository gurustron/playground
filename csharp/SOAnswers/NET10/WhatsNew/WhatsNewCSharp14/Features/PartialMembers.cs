namespace WhatsNewCSharp14.Features;

public partial class PartialMembers
{
    // No this or base allowed, only on the implementation
    public partial PartialMembers() /* : this(1) */ /* : base() */;
    public partial PartialMembers(int _);

    public partial event EventHandler<EventArgs> PartialEvent;
}

public partial class PartialMembers
{
    public partial PartialMembers() { }
    public partial PartialMembers(int _) { }
    
    public partial event EventHandler<EventArgs> PartialEvent
    {
        add => Console.WriteLine(value);
        remove => Console.WriteLine(value);
    }
}