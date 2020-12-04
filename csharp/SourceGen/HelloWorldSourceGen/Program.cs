using System;

namespace HelloWorldSourceGen
{
    class Program
    {
        static void Main(string[] args)
        {
            // GeneratedNamespace.GeneratedClass.GeneratedMethod();
            Console.WriteLine(new ToStringable{Name = nameof(ToStringable)});
            Console.WriteLine(new AlreadyHasToString{Name = nameof(AlreadyHasToString)});
        }
    }
}