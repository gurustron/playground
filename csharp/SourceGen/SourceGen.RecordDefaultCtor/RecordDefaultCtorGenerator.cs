using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SourceGen.RecordDefaultCtor
{
    [Generator]
    public class RecordDefaultCtorGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new RecordSyntaxReceiver());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            if (context.SyntaxReceiver is not RecordSyntaxReceiver receiver)
            {
                throw new Exception();
            }

            foreach (var recordDeclaration in receiver.RecordDeclarations)
            {
                var semanticModel = context.Compilation.GetSemanticModel(recordDeclaration.SyntaxTree);
                var namespaceDeclaration = recordDeclaration.Parent as NamespaceDeclarationSyntax;
                var recordName = recordDeclaration.Identifier.ToString();
                var @namespace = namespaceDeclaration?.Name.ToString() ?? "global";
                var code = @$"
namespace {@namespace}
{{
    {recordDeclaration.Modifiers.ToString()} record {recordName}
    {{
        public {recordName}() : this(default(string))
        {{
        }}
    }}
}}
";
            }
        }
    }
}
