using System;
using System.Collections.Generic;
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
        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new CustomSyntaxReceiver());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            Console.WriteLine("HelloWorld");

            var msgs = new List<string>();
            if (context.SyntaxReceiver is not CustomSyntaxReceiver receiver)
            {
                msgs.Add("Not a CustomSyntaxReceiver");
            }
            else
            {
                // using var fileStream = File.CreateText("/home/gurustron/Projects/logs.txt");
                // fileStream.WriteLine("42");

                foreach (var classDeclarationSyntax in receiver.PartialClasses)
                {
                    msgs.Add(classDeclarationSyntax.Identifier.Text);
                }
            }
            
            var res = string.Join(Environment.NewLine, msgs.Select(m => $"Console.WriteLine(\"{m}\");"));

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
        
        private class CustomSyntaxReceiver : ISyntaxReceiver
        {
            public List<ClassDeclarationSyntax> PartialClasses { get; } = new();
            public List<string> Debug { get; set; } = new();

            public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
            {
                if (syntaxNode is ClassDeclarationSyntax record)
                {
                    if (!record.Modifiers.Any(m => m.IsKind(SyntaxKind.PartialKeyword)))
                    {
                        return;
                    }

                    var methods = record.Members.OfType<MethodDeclarationSyntax>();
                    var hasOverridenToString = false;
                    foreach (var method in methods)
                    {
                        Debug.Add($"\t\t{method.Identifier.Text} | {string.Join("__", method.Modifiers.Select(m => m.Kind()))}");

                        if (method.Identifier.Text != nameof(ToString))
                        {
                            continue;
                        }

                        if (!method.Modifiers.Any(m => m.IsKind(SyntaxKind.OverrideKeyword)))
                        {
                            continue;
                        }

                        if (method.ParameterList.Parameters.Any())
                        {
                            continue;
                        }

                        if (method.TypeParameterList != null)
                        {
                            continue;
                        }

                        hasOverridenToString = true;
                    }

                    if (!hasOverridenToString)
                    {
                        PartialClasses.Add(record); 
                    }
                }
            }
        }
    }
}