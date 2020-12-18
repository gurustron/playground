// using System.Collections.Generic;
// using System.Diagnostics;
// using Microsoft.CodeAnalysis;
// using Microsoft.CodeAnalysis.CSharp.Syntax;
//
// namespace SourceGen.Traits
// {
//     [Generator]
//     public class TraitGenerator:ISourceGenerator
//     {
//         public void Initialize(GeneratorInitializationContext context)
//         {
//             context.RegisterForSyntaxNotifications(() => new ClassSyntaxReceiver());
//         }
//
//         public void Execute(GeneratorExecutionContext context)
//         {
//             if (context.SyntaxReceiver is not ClassSyntaxReceiver receiver)
//             {
//                 context.ReportDiagnostic(Diagnostic.Create(TraitDiagnostics.GeneralWarning, Location.None, $"Not a {nameof(ClassSyntaxReceiver)}"));
//                 return;
//             }
//
//             foreach (var cls in receiver.AllClasses)
//             {
//                 // context.Compilation.GetSemanticModel(cls.SyntaxTree)
//             }
//         }
//     }
//
//     public class ClassSyntaxReceiver : ISyntaxReceiver
//     {
//         public List<ClassDeclarationSyntax> AllClasses { get; } = new();
//
//         public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
//         {
//             switch (syntaxNode)
//             {
//                 case ClassDeclarationSyntax cls:
//                     VisitClassDeclarationSyntax(cls);
//                     break;
//             }
//         }
//
//         private void VisitClassDeclarationSyntax(ClassDeclarationSyntax cls) => AllClasses.Add(cls);
//     }
// }