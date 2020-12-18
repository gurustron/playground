using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SourceGen
{
    [Generator]
    public class TraitGenerator:ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new ClassSyntaxReceiver());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            
        }
    }

    public class ClassSyntaxReceiver : ISyntaxReceiver
    {
        public List<ClassDeclarationSyntax> AllClasses { get; } = new();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            switch (syntaxNode)
            {
                case ClassDeclarationSyntax cls:
                    VisitClassDeclarationSyntax(cls);
                    break;
            }
        }

        private void VisitClassDeclarationSyntax(ClassDeclarationSyntax cls) => AllClasses.Add(cls);
    }
}