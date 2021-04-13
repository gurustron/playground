using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace GeneratedNamespace{
    partial class GeneratedClass
    {
        [DisplayName(nameof(GeneratedPropName))]
        public string VerySpecificPropName { get; set; }
    }
}

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
            Console.WriteLine(nameof(GeneratedNamespace.GeneratedClass.GeneratedMethod));
            Console.WriteLine(typeof(GeneratedNamespace.GeneratedClass).GetProperty(nameof(GeneratedNamespace.GeneratedClass.GeneratedPropName))!.GetCustomAttribute<DisplayAttribute>()?.Name);
            // // Console.WriteLine(new ToStringable{Name = nameof(ToStringable)});
            // // Console.WriteLine(new AlreadyHasToString{Name = nameof(AlreadyHasToString)});
            // //
            // //
            // var testRecord = new TestRecord
            // {
            //     Foo = new NotPartialRecord("a")
            // };
            // Console.WriteLine(testRecord);
            // var testRecord3 = new TestRecord3<int>
            // {
            //     Foo = "new NotPartialRecord(\"a\")",
            // };
            // Console.WriteLine(testRecord3);
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