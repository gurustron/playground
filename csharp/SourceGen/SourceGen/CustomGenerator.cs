using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace HelloWorldSourceGen
{
    [Generator]
    public class CustomGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context) {}

        public void Execute(GeneratorExecutionContext context)
        {
            Console.WriteLine("HelloWorld");
            context.AddSource("myGeneratedFile.cs", SourceText.From(@"
namespace GeneratedNamespace
{
    using System;
    public class GeneratedClass
    {
        public static void GeneratedMethod()
        {
            // generated code

            Console.WriteLine(""Hello World"");

        }
    }
}", Encoding.UTF8));

            var types = context.Compilation.Assembly.TypeNames;


            using var fileStream = File.CreateText("/home/gurustron/Projects/logs.txt");
            fileStream.WriteLine("42");
            foreach (var type in types)
            {
                fileStream.WriteLine(type);
                
            }
        }
    }
}