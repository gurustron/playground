using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SourceGen.RecordDefaultCtor
{
    internal class RecordSyntaxReceiver : ISyntaxReceiver
    {
        public List<RecordDeclarationSyntax> RecordDeclarations { get; } = new();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is RecordDeclarationSyntax record)
            {
                //TODO: filter out nodes with compilation errors

                if (!record.Modifiers.Any(m => m.IsKind(SyntaxKind.PartialKeyword)))
                {
                    return;
                }

                if (record.ChildNodes().Any(HasDefaultCtor))
                {
                    return;
                }

                if (record.ParameterList is null)
                {
                    return;
                }

                RecordDeclarations.Add(record);
            }

            bool HasDefaultCtor(SyntaxNode node)
            {
                if (node is ConstructorDeclarationSyntax ctr && !ctr.ParameterList.ChildNodes().Any())
                {
                    return true;
                }

                return false;
            }
        }
    }
}
