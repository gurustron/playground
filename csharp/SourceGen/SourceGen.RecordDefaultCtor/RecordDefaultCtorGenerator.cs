using System;
using System.Collections.Generic;
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
                if (recordDeclaration.ParameterList is null)
                {
                    continue;
                }

                var semanticModel = context.Compilation.GetSemanticModel(recordDeclaration.SyntaxTree);
                var namespaceDeclaration = recordDeclaration.Parent as NamespaceDeclarationSyntax;
                var recordName = recordDeclaration.Identifier.ToString();
                var @namespace = namespaceDeclaration?.Name.ToString() ?? "global";

                // process parameters
                List<string> @params = new();
                var syntaxNodes = recordDeclaration.ParameterList.ChildNodes().ToList();
                // var x =
                //     semanticModel.GetTypeInfo((syntaxNodes.First() as ParameterSyntax).Type).Type as INamedTypeSymbol;
                // var y = x.ContainingNamespace.ToString();
                var array = syntaxNodes.OfType<ParameterSyntax>().ToArray();
                // var i = array[0];
                // var gen = array[1];
                foreach (var parameter in syntaxNodes.OfType<ParameterSyntax>())
                {
                    // TODO: handle generics
                    var typeSymbol = semanticModel.GetTypeInfo(parameter.Type).Type;
                    @params.Add($"default({GetFullyQualifiedTypeName(typeSymbol)})");
                }

                var code =
// @formatter:off
@$"namespace {@namespace}
{{
    {recordDeclaration.Modifiers.ToString()} record {recordName}{recordDeclaration.TypeParameterList?.ToString()}
    {{
        public {recordName}() : this({string.Join(",", @params)})
        {{
        }}
    }}
}}";
// @formatter:on
                context.AddSource($"{@namespace}.{recordName}.Ctor.{Guid.NewGuid():N}.cs", code);
            }
        }

        private static string GetFullyQualifiedTypeName(ITypeSymbol typeSymbol) =>
            typeSymbol switch
            {
                ITypeParameterSymbol => typeSymbol.Name,
                INamedTypeSymbol {IsGenericType: true} nts =>
                    $"{nts.ContainingNamespace}.{nts.Name}<{string.Join(",",nts.TypeArguments.Select(GetFullyQualifiedTypeName))}>",
                INamedTypeSymbol nts => $"{nts.ContainingNamespace}.{nts.Name}",
                _ => throw new Exception($"Unsupported type {typeSymbol?.GetType()}.")
            };
    }
}
