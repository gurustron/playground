namespace HelloWorldSourceGen
{
    public partial record TestRecord(string Foo);

    public partial record TestRecord1(string Foo, int Bar);

    public partial record TestRecord2(string Foo, int Bar)
    {

    }

    public record NotPartialRecord(string Foo);

    // public record MyRecord(string S)
    // {
    //     public MyRecord() : this(default(string))
    //     {
    //
    //     }
    // }
}
