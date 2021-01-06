using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SourceGen.RecordDefaultCtor
{
    internal class RecordSyntaxReceiver : ISyntaxReceiver
    {
        public List<RecordDeclarationSyntax> Records { get; } = new();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is RecordDeclarationSyntax record)
            {
                if (!record.Modifiers.Any(m => m.IsKind(SyntaxKind.PartialKeyword)))
                {
                    return;
                }

                if (record.ChildNodes().Any(node => node is ConstructorDeclarationSyntax))
                {
                    return;
                }

                Records.Add(record);
            }
        }
    }
}
