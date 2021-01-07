using System.Collections.Generic;

namespace HelloWorldSourceGen
{
    public partial record TestRecord(NotPartialRecord Foo);

    public partial record TestRecord1(string Foo, int Bar);

    public partial record TestRecord2(string Foo, int Bar)
    {

    }
    public partial record TestRecord3<T>(string Foo);
    
    public partial record TestRecord
    {
    }
    public record NotPartialRecord(string Foo);
    public record NotPartialRecord1(string Foo, List<int> Ints);
    public record NotPartialRecord2<T>(string Foo, List<T> Ints);
    public record NotPartialRecord3<T>(string Foo, T Ints);

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
