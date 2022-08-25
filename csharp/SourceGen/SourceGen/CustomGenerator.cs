using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace SourceGen
{
    [Generator]
    public class CustomGenerator : ISourceGenerator
    {
        private static readonly DiagnosticDescriptor CustomGeneratorWarning = new DiagnosticDescriptor(id: "MYCUSTOMGEN001",
            title: "CustomGeneratorWarning",
            messageFormat: "Foo '{0}'.",
            category: "CustomGenerator",
            DiagnosticSeverity.Warning,
            isEnabledByDefault: true);
        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new CustomSyntaxReceiver());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            // context.ReportDiagnostic(Diagnostic.Create(CustomGeneratorWarning, Location.None, "Bar"));
            var msgs = new List<string>();
            if (context.SyntaxReceiver is not CustomSyntaxReceiver receiver)
            {
                msgs.Add("Not a CustomSyntaxReceiver");
            }
            else
            {
                foreach (var classDeclarationSyntax in receiver.PartialClasses)
                {
                    // msgs.Add(classDeclarationSyntax.Identifier.Text);
                    // context.ReportDiagnostic(Diagnostic.Create(CustomGeneratorWarning, Location.None, ((NamespaceDeclarationSyntax) classDeclarationSyntax.Parent).Name));

                    var props = classDeclarationSyntax
                        .Members
                        .OfType<PropertyDeclarationSyntax>()
                        .Select(p => $"{p.Identifier.Text}: {{{p.Identifier.Text}}}");
                    var text = $@"
namespace {((NamespaceDeclarationSyntax) classDeclarationSyntax.Parent).Name}
{{
    using System;
    public partial class {classDeclarationSyntax.Identifier.Text}
    {{
        public override string ToString()
        {{
            return $""{string.Join(" ", props)}"";
        }}
    }}
}}";

                    context.AddSource($"{classDeclarationSyntax.Identifier.Text}.ToString.cs", SourceText.From(
                        text, Encoding.UTF8));

                    var propertyDeclarationSyntax = receiver.Props.Where(syntax => syntax.Identifier.Text == "VerySpecificPropName").First();
                    var semanticModel = context.Compilation.GetSemanticModel(propertyDeclarationSyntax.SyntaxTree);
                    
                    var attributeListSyntaxes = receiver.Props.Where(syntax => syntax.Identifier.Text == "VerySpecificPropName")
                        .Select(syntax => syntax.AttributeLists.First())
                        .First();
                    
                    var declaredSymbol = semanticModel.GetDeclaredSymbol(attributeListSyntaxes);
                }
            }

            var res = string.Join(Environment.NewLine, msgs.Select(m => $"Console.WriteLine(\"{m}\");"));

            var helloWorldGen = $@"
namespace GeneratedNamespace
{{
    using System;
    public partial class GeneratedClass
    {{
public string GeneratedPropName {{ get; set; }}
        public static void GeneratedMethod()
        {{
            // generated code
            Console.WriteLine();
            {res}
        }}
    }}
}}";
            context.AddSource("myGeneratedFile.cs", SourceText.From(helloWorldGen, Encoding.UTF8));
        }
        
        private class CustomSyntaxReceiver : ISyntaxReceiver
        {
            public List<ClassDeclarationSyntax> PartialClasses { get; } = new();
            public List<PropertyDeclarationSyntax> Props { get; } = new();
            public List<string> Debug { get; } = new();

            public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
            {
                    if (syntaxNode is PropertyDeclarationSyntax pds)
                    {
                        Props.Add(pds);
                    }
                if (syntaxNode is ClassDeclarationSyntax record)
                {
                    if (record.Modifiers.Any(m => m.IsKind(SyntaxKind.StaticKeyword)))
                    {
                        return;
                    }

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