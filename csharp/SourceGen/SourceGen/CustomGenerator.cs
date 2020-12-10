using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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

            var msgs = new List<string>();
            var types = context.Compilation.Assembly.TypeNames;


            using var fileStream = File.CreateText("/home/gurustron/Projects/logs.txt");
            // fileStream.WriteLine("42");

            var res = string.Join(Environment.NewLine,
                msgs.Select(m => $"Console.WriteLine(\"{m}\");"));
            context.AddSource("myGeneratedFile.cs", SourceText.From(
                $@"
namespace GeneratedNamespace
{{
    using System;
    public class GeneratedClass
    {{
        public static void GeneratedMethod()
        {{
            // generated code

           {res}

        }}
    }}
}}", Encoding.UTF8));
        }
        
        private class PartialRecordsSyntaxReceiver : ISyntaxReceiver
        {
            public List<ClassDeclarationSyntax> PartialClasses { get; } = new();

            public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
            {
                if (syntaxNode is ClassDeclarationSyntax record &&
                    record.Modifiers.Any(m => m.IsKind(SyntaxKind.PartialKeyword)))
                {
                    PartialClasses.Add(record);
                }
            }
        }
    }
}