using System;

namespace HelloWorldSourceGen
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--------------------------------");
            GeneratedNamespace.GeneratedClass.GeneratedMethod();
            Console.WriteLine("--------------------------------");
            Console.WriteLine();
            // Console.WriteLine(new ToStringable{Name = nameof(ToStringable)});
            // Console.WriteLine(new AlreadyHasToString{Name = nameof(AlreadyHasToString)});
            //
            //
            var testRecord = new TestRecord
            {
                Foo = new NotPartialRecord("a")
            };
            Console.WriteLine(testRecord);
            var testRecord3 = new TestRecord3<int>
            {
                Foo = "new NotPartialRecord(\"a\")",
            };
            Console.WriteLine(testRecord3);
        }

        // public class MyTrait
        // {
        //     public void Do()
        //     {
        //         Console.WriteLine("Trait!");
        //     }
        // }
        //
        // [Trait(typeof(MyTrait))]
        // public partial class MyClass
        // {
        //     
        // }
    }
}