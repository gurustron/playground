namespace HelloWorldSourceGen
{
    public partial record TestRecord(string Foo);

    public partial record TestRecord1(string Foo, int Bar);

    public partial record TestRecord2(string Foo, int Bar)
    {

    }
    //
    // public partial record TestRecord
    // {
    // }
    public record NotPartialRecord(string Foo);

    public record NoCtor
    {
        public string S { get; init; }
    }

    public record MyRecord(string S)
    {
        public MyRecord() : this(default(string))
        {
    
        }
    }
}
