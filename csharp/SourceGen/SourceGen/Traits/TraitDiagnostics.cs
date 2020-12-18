using Microsoft.CodeAnalysis;

namespace SourceGen.Traits
{
    public static class TraitDiagnostics
    {
        public static readonly DiagnosticDescriptor GeneralWarning = new DiagnosticDescriptor(
            id: "TRAITGEN001",
            title: "Trait generation warning",
            messageFormat: "Foo '{0}'.",
            category: "TraitGeneration",
            DiagnosticSeverity.Warning,
            isEnabledByDefault: true);
    }
}