using System;
using SourceGen.Traits.Core;

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
            Console.WriteLine(new ToStringable{Name = nameof(ToStringable)});
            Console.WriteLine(new AlreadyHasToString{Name = nameof(AlreadyHasToString)});
            Console.WriteLine(new NonPartial());
        }

        public class MyTrait
        {
            public void Do()
            {
                Console.WriteLine("Trait!");
            }
        }

        [Trait(typeof(MyTrait))]
        public partial class MyClass
        {
            
        }
        
    }
}